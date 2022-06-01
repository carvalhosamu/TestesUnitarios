using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);

        }

        [Fact(DisplayName = "Adicionar Item Existente no Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(c => c.ProdutoId == produtoId)!.Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemAcimaPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act && Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem)) ;

        }

        [Fact(DisplayName = "Adicionar Item Pedido Abaixo do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemAbaixoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", Pedido.MIN_UNIDADES_ITEM - 1, 100);

            // Act && Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));

        }

        [Fact(DisplayName = "Adicionar Item Existente no Pedido Acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistenteSomaAcimaPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            var pedidoItem01 = new PedidoItem(produtoId, "Produto Teste", Pedido.MIN_UNIDADES_ITEM, 100);
            var pedidoItem02 = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM, 100);

            // Act && Assert
            pedido.AdicionarItem(pedidoItem01);
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem02));

        }

        [Fact(DisplayName = "Editar Item não existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExisteNoPedido_DeveRetornarException()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", Pedido.MIN_UNIDADES_ITEM, 100);

            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }


        [Fact(DisplayName = "Editar quantidade de Item existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemExisteNoPedido_DeveAtualizarAQuantidade()
        {
            // Arrange 
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
            
            pedido.AdicionarItem(pedidoItem);

            // Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Assert
            Assert.Equal(pedidoItemAtualizado.Quantidade, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId)!.Quantidade);
        }

    }
}

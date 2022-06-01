using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;

        public decimal ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }
        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public PedidoStatus PedidoStatus { get; private set; }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValorTotal());
        }

        private bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem item)
        {
            int quantidadeItens = item.Quantidade;
            
            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.First(c => c.ProdutoId == item.ProdutoId);
                quantidadeItens += itemExistente.Quantidade;  
            }

            if (quantidadeItens > MAX_UNIDADES_ITEM)
                throw new DomainException($"A quantidade não pode exceder a {MAX_UNIDADES_ITEM} itens. ");

            if (quantidadeItens < MIN_UNIDADES_ITEM)
                throw new DomainException($"A quantidade deve ser maior que {MIN_UNIDADES_ITEM} itens. ");
        }
        
        public void AdicionarItem(PedidoItem item)
        {
            ValidarQuantidadeItemPermitida(item);

            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.First(c => c.ProdutoId == item.ProdutoId);
               
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;
                _pedidoItems.Remove(itemExistente);
            }
   
            _pedidoItems.Add(item);
            CalcularValorPedido();
        }

        private void ValidarPedidoExistente(PedidoItem item)
        {
            if (!PedidoItemExistente(item))
            {
                throw new DomainException("O item não existe no pedido");
            }
        }

        public void AtualizarItem(PedidoItem item)
        {
            ValidarPedidoExistente(item);
            
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId,
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }

   
}

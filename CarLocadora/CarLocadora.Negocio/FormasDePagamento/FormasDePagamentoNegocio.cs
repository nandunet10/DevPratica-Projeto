﻿using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.FormasDePagamento
{
    public class FormasDePagamentoNegocio : IFormasDePagamentoNegocio
    {
        private readonly Context _context;

        public FormasDePagamentoNegocio(Context context)
        {
            _context = context;
        }

        public void Alterar(FormasDePagamentoModel model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(FormasDePagamentoModel model)
        {
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public List<FormasDePagamentoModel> ObterLista() => _context.FormasDePagamento.ToList();

    }
}
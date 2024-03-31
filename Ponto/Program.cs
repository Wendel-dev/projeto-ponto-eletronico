﻿using System;
using System.Windows.Forms;
using Ponto.DataBase;
using Ponto.Interface;

namespace Ponto
{
    public static class Program
    {
        public static CriarAtividade criarAtividade;
        public static atualizar_data cadastrarInicio ;
        public static Cadastro cadastrarUsuario;
        public static PaginaInicial paginaInicial ;
        public static Login login ;
        public static RedefinicaoDeSenha redefinirSenha;
        public static EditActivity editActivity ;
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new BancoDeDados();
            new RegistrationTable();
            RunApplication.login(true);/*
            new teste();*/
        }
    }
}

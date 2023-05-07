using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class frm_biblioteca : Form
    {
        public frm_biblioteca()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dataHora = DateTime.Now;
            lbl_datetime.Text = $"Data: {dataHora.ToShortDateString()}. Hora: {dataHora.ToLongTimeString()}";
        }

        private void frm_biblioteca_Load(object sender, EventArgs e)
        {
            timer1_Tick(e, e);
        }

        private void bt_CadastroLivro(object sender, EventArgs e)
        {
            try
            {
                Database obj = new Database();
                ArrayList arrayList = new ArrayList();

                arrayList.Add(txb_livro.Text);
                arrayList.Add(txb_autor.Text);
                arrayList.Add(txb_editora.Text);
                arrayList.Add(txb_anodepublicacao.Text);
                arrayList.Add(txb_numerodepaginas.Text);
                arrayList.Add(txb_classificacao.Text);
                arrayList.Add(txb_datadeaquisicao.Text);
                arrayList.Add(txb_observacao.Text);

                if (obj.Insert(arrayList))
                    MessageBox.Show("Livro cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Erro ao cadastrar Livro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ocorrido: {ex.Message}");
            }
        }

        private void btn_SalvaLivro(object sender, EventArgs e)
        {
            try
            {
                Database obj = new Database();
                ArrayList arrayList = new ArrayList();

                arrayList.Add(txb_codlivro.Text);
                arrayList.Add(txb_editar_livro.Text);
                arrayList.Add(txb_editar_autor.Text);
                arrayList.Add(txb_editar_editora.Text);
                arrayList.Add(txb_editar_anodepublicacao.Text);
                arrayList.Add(txb_editar_paginas.Text);
                arrayList.Add(txb_editar_classificacao.Text);
                arrayList.Add(txb_editar_datadeaquisicao.Text);
                arrayList.Add(txb_editar_observacao.Text);

                if (obj.Update(arrayList))
                {
                    MessageBox.Show("Livro atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabPage2_Enter(e, e);
                }

                else
                    MessageBox.Show("Erro ao atualizar Livro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ocorrido: {ex.Message}");
            }
        }

        private void btn_ApagaLivro(object sender, EventArgs e)
        {
            try
            {
                Database obj = new Database();
                int codLivro = int.Parse(tb_Excluir.Text);

                if (obj.Delete(codLivro))
                {
                    MessageBox.Show("Livro apagado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dg_excluir_Enter(e, e);
                }
                else
                    MessageBox.Show("Erro ao apagar Livro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ocorrido: {ex.Message}");
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            Database obj = new Database();
            dg_editar.DataSource = obj.ListaGrid();
            dg_excluir.DataSource = obj.ListaGrid();
        }

        private void dg_excluir_Enter(object sender, EventArgs e)
        {
            Database obj = new Database();
            dg_excluir.DataSource = obj.ListaGrid();
        }

        private void btn_Pesquisar_Click(object sender, EventArgs e)
        {
            Database obj = new Database();
            string sql;

            if (rb_Livro.Checked)
            {
                sql = @"SELECT CODLIVRO as [Código do Livro], LIVRO as Livro, AUTOR as Autor, 
                        EDITORA as Editora, ANODEPUBLICACAO as [Ano de Publicação] 
                        FROM ESTANTE WHERE LIVRO LIKE @VALOR ";

                dg_pesquisar.DataSource = obj.Pesquisar(sql, "%" + tbVPesquisa.Text + "%");
            }
            else
            {
                sql = @"SELECT CODLIVRO as [Código do Livro], LIVRO as Livro, AUTOR as Autor, 
                        EDITORA as Editora, ANODEPUBLICACAO as [Ano de Publicação] 
                        FROM ESTANTE WHERE CODLIVRO = @VALOR ";

                dg_pesquisar.DataSource = obj.Pesquisar(sql, tbVPesquisa.Text);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AnalizadorSintatico
{
    public partial class AnalizadorSintatico : Form
    {
        DataTable analisis = new DataTable();
        List<char> delimitadores = new List<char>(new char[] { '(', ')', ':', ';', '[', ']', '{', '}', '"'});
        List<char> constante = new List<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
        List<char> identificadores = new List<char>(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' });
        List<char> operadores = new List<char>(new char[] { '+', '-', '*', '/', '<', '>', '=', '!'});

        public AnalizadorSintatico()
        {
            InitializeComponent();
        }

        private void AnalizadorSintatico_Load(object sender, EventArgs e)
        {
            analisis.Columns.Add("Token", typeof(char));
            analisis.Columns.Add("Lexema", typeof(string));
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            analisis.Clear();
            tblResultado.DataSource = null;
            tblResultado.Refresh(); 
           
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            analisis.Clear();

            List<char> _elementos = txtCodigo.Text.Replace(" ", "").ToCharArray().ToList();

            if (_elementos.Count > 0)
            {
                DataRow fila;

                foreach (char elemento in _elementos)
                {
                    fila = analisis.NewRow();

                    if (constante.Contains(elemento))
                    {
                        fila["Token"] = elemento;
                        fila["Lexema"] = "CONSTANTE";
                    }
                    else if (delimitadores.Contains(elemento))
                    {
                        fila["Token"] = elemento;
                        fila["Lexema"] = "DELIMITADOR";
                    }
                    else if (operadores.Contains(elemento))
                    {
                        fila["Token"] = elemento;
                        fila["Lexema"] = "OPERADOR";
                    }
                    else if (identificadores.Contains(elemento.ToString().ToUpper()[0]))
                    {
                        fila["Token"] = elemento;
                        fila["Lexema"] = "IDENTIFICADOR";
                    }
                    else
                    {
                        fila["Token"] = elemento;
                        fila["Lexema"] = "ERROR";
                    }

                    analisis.Rows.Add(fila);
                }

                tblResultado.DataSource = analisis;
                tblResultado.Refresh();
            }
            else
            {
                tblResultado.DataSource = null;
                tblResultado.Refresh();
            }
        }
    }
}

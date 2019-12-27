using System;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading;
using System.Threading.Tasks;
//using System.Threading.Tasks;

namespace FormWithButton
{
    public class Form1 : Form
    {
        private Button button1;
        private FlowLayoutPanel panel1;
        private FlowLayoutPanel panel2;

        private db banco;

        public Form1()
        {
            banco = new db("hello.db");


            panel1 = new FlowLayoutPanel();
            panel1.FlowDirection = FlowDirection.RightToLeft;
            panel1.Dock = DockStyle.Bottom;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.AutoSize = true;

            panel2 = new FlowLayoutPanel();
            panel2.FlowDirection = FlowDirection.TopDown;
            panel2.Dock = DockStyle.Fill;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.AutoSize = true;

            // panel3 = new FlowLayoutPanel();
            // panel3.FlowDirection = FlowDirection.TopDown;
            // panel3.Dock = DockStyle.Right;


            panel1.Controls.Add(NewButton("Excluir"));
            panel1.Controls.Add(NewButton("Inserir"));

            panel2.Controls.Add(NewField("Nome","T1_Nome"));
            panel2.Controls.Add(NewField("Valor","T1_Valor"));
            panel2.Controls.Add(NewField("Telefone","T1_Telefone"));
            panel2.Controls.Add(NewField("Endereço","T1_Endereco"));
            panel2.Controls.Add(NewField("CPF","T1_CPF"));




            this.Controls.Add(panel1);
            this.Controls.Add(panel2);

        }

        public Field NewField(string descricao,string nome)
        {
            Field txt = new Field(descricao);
            txt.text_box.Name = nome;
            txt.Leave += new System.EventHandler(Validar);
            return txt;
        }
        public Button NewButton(string txt)
        {
            button1 = new Button();
            button1.Text = txt;

            return button1;
        }

        static void Validar(object sender, System.EventArgs e)
        {
            Field field = sender as Field;
            string txt = field.text_box.Text;
            bool res = validacao(txt).Result;
            
            if(!res)
                MessageBox.Show("Valor não pode ser menor que zero.", "Aviso", MessageBoxButtons.OK);
            
        }

        private static async Task<bool> validacao(string txt)
        {
            var globals = new Globals { x = Convert.ToInt32(txt)};

            bool result = await CSharpScript.EvaluateAsync<bool>("x > 0", globals: globals);


            return result;
        }

        public class Globals
        {
            public int x;
            public int Y;
        }
        static class Program
        {
            [STAThread]
            static void Main(string[] args)
            {
                Application.EnableVisualStyles();
                Form1 form1 = new Form1();
                form1.Left = 50;
                form1.Top = 10;
                form1.AutoSize = true;


                Application.Run(form1);
            }

        }
    }
}


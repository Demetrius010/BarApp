using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;


namespace Bar
{
    public partial class Form1 : Form              ///42; 120; 138 ///СОХРАНЯЕТ ВСЕ ЛИСТЫ??
    {
        List<String> userIngredients = new List<string>();



        public Form1()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {

        }

        private void chartButton_Click(object sender, EventArgs e)
        {

        }

        private void chartPieButton_Click(object sender, EventArgs e)
        {

        }


        /*                   ACTIONS IN THE LEFT PANEL             */
        private void buttonAlcohol_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonAlcohol.Top;
            dataGridView.DataSource = alcoholBindingSource;
            alcoholBindingNavigator.BindingSource = alcoholBindingSource;
        }

        private void buttonSyrup_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonSyrup.Top;
            dataGridView.DataSource = syrupBindingSource;
            alcoholBindingNavigator.BindingSource = syrupBindingSource;
        }

        private void buttonSoda_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonSoda.Top;
            dataGridView.DataSource = sodaBindingSource;
            alcoholBindingNavigator.BindingSource = sodaBindingSource;
        }

        private void buttonJuice_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonJuice.Top;
            dataGridView.DataSource = juiceBindingSource;
            alcoholBindingNavigator.BindingSource = juiceBindingSource;
        }

        private void buttonFruit_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonFruit.Top;
            dataGridView.DataSource = fruitBindingSource;
            alcoholBindingNavigator.BindingSource = fruitBindingSource;

        }

        private void buttonOther_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonOther.Top;
            dataGridView.DataSource = otherBindingSource;
            alcoholBindingNavigator.BindingSource = otherBindingSource;
        }


        /*                  CONTROL APPLICATION                 */
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimazeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void fullScreenButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }


        /*                  MOVE APPLICATION                 */
        bool appMove;
        int moveValX;
        int moveValY;

        private void hatApplicationPanel_MouseDown(object sender, MouseEventArgs e)
        {
            appMove = true;
            moveValX = e.X;
            moveValY = e.Y;
        }

        private void hatApplicationPanel_MouseUp(object sender, MouseEventArgs e)
        {
            appMove = false;
        }

        private void hatApplicationPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (appMove)
            {
                this.SetDesktopLocation(MousePosition.X - moveValX - panel1.Width, MousePosition.Y - moveValY); // отнимаем смещение курсора от центра приложения (для X еще и ширину ComeAndDrink) 
            }
        }


        private void alcoholBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.alcoholBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.barDatabaseDataSet);

        }

        /*                  LOAD FILE (TABLE)              */
        private void Form1_Load(object sender, EventArgs e)
        {
            alcoholBindingNavigator.Renderer = new MyToolStripSystemRenderer();
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Drinks' table. You can move, or remove it, as needed.
            this.drinksTableAdapter.Fill(this.barDatabaseDataSet.Drinks);
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Alcohol' table. You can move, or remove it, as needed.
            this.alcoholTableAdapter.Fill(this.barDatabaseDataSet.Alcohol);
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Other' table. You can move, or remove it, as needed.
            this.otherTableAdapter.Fill(this.barDatabaseDataSet.Other);
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Syrup' table. You can move, or remove it, as needed.
            this.syrupTableAdapter.Fill(this.barDatabaseDataSet.Syrup);
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Soda' table. You can move, or remove it, as needed.
            this.sodaTableAdapter.Fill(this.barDatabaseDataSet.Soda);
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Juice' table. You can move, or remove it, as needed.
            this.juiceTableAdapter.Fill(this.barDatabaseDataSet.Juice);
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Fruit' table. You can move, or remove it, as needed.
            this.fruitTableAdapter.Fill(this.barDatabaseDataSet.Fruit);

        }

        private void cocktailsDBButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DrinksTable = this.barDatabaseDataSet.Drinks;
                getAllUserIngridients();

                /*foreach (string item in userIngredients)
                {
                    Console.WriteLine(item);
                }*/
                //Console.WriteLine(getSearchString());

                foreach (DataRow foundRows in DrinksTable.Select(getSearchString()))
                {
                    Console.WriteLine(foundRows["strDrink"]);
                }
                /*foreach(DataRow row in DrinksTable.Rows)
                {
                    foreach(DataColumn col in DrinksTable.Columns)
                }*
                
                /*string link = DrinksTable.Rows[0]["strDrinkThumb"].ToString();    
                Console.WriteLine(link);
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(link);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                pictureBox2.Image = image;*/

            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }

        private string getSearchString()//("strIngredient IS NULL OR IN ('', '', '')) AND"
        {
            string searchString = ""; 
            for (int ingredientN = 1; ingredientN < 16; ingredientN++)
            {
                searchString += "(strIngredient" + ingredientN.ToString() + " IS NULL OR " + "strIngredient" + ingredientN.ToString() + " IN (";
                foreach (string userIngredient in userIngredients)
                {
                    searchString += "'" + userIngredient + "', ";
                }
                searchString += ")) AND ";
            }
            searchString = searchString.Substring(0, searchString.Length - 4); // обрезаем лишний "AND "
            return searchString;
        }

        private void getAllUserIngridients()
        {
            collectDataFromTable(this.barDatabaseDataSet.Alcohol);
            collectDataFromTable(this.barDatabaseDataSet.Syrup);
            collectDataFromTable(this.barDatabaseDataSet.Soda);
            collectDataFromTable(this.barDatabaseDataSet.Juice);
            collectDataFromTable(this.barDatabaseDataSet.Fruit);
            collectDataFromTable(this.barDatabaseDataSet.Other);

        }

        private void collectDataFromTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                userIngredients.Add(row["Name"].ToString());
            }

        }

        private void MakeButton_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {

        }
    }
}

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


namespace Bar
{
    public partial class Form1 : Form              ///42; 120; 138 ///СОХРАНЯЕТ ВСЕ ЛИСТЫ??
    {


        
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
            //this.alcoholTableAdapter.Fill(this.barDatabaseDataSet.Alcohol);
        }

        private void buttonSyrup_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonSyrup.Top;
            dataGridView.DataSource = syrupBindingSource;
            //this.syrupTableAdapter.Fill(this.barDatabaseDataSet.Syrup);
        }

        private void buttonSoda_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonSoda.Top;
            dataGridView.DataSource = sodaBindingSource;
            //this.sodaTableAdapter.Fill(this.barDatabaseDataSet.Soda);
        }

        private void buttonJuice_Click(object sender, EventArgs e)
        {  
            panelLeft.Top = buttonJuice.Top;
            dataGridView.DataSource = juiceBindingSource;
           // this.juiceTableAdapter.Fill(this.barDatabaseDataSet.Juice);
        }

        private void buttonFruit_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonFruit.Top;
            dataGridView.DataSource = fruitBindingSource;
           // this.fruitTableAdapter.Fill(this.barDatabaseDataSet.Fruit);

        }

        private void buttonOther_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonOther.Top;
            dataGridView.DataSource = otherBindingSource;
            //this.otherTableAdapter.Fill(this.barDatabaseDataSet.Other);
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

        }

        private void MakeButton_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {

        }
    }
}

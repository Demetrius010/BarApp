﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;


namespace Bar
{
    public partial class Form1 : Form
    {
        List<String> userIngredients = new List<string>(); // Список всех ингридиентов пользователя
        Size defaultScreenSize;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            defaultScreenSize = this.Size;
            gridCondition(false);
            cocktailInfo.Enabled = false;
            cocktailInfo.Visible = false;
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

        /*                  LOAD              */
        private void Form1_Load(object sender, EventArgs e)
        {
            
            // TODO: This line of code loads data into the 'barDatabaseDataSet.Ingredients' table. You can move, or remove it, as needed.
            alcoholBindingNavigator.Renderer = new MyToolStripSystemRenderer();
            this.drinksTableAdapter.Fill(this.barDatabaseDataSet.Drinks);// This line of code loads data into the 'barDatabaseDataSet.Drinks' table.
            this.ingredientsTableAdapter.Fill(this.barDatabaseDataSet.Ingredients);// TODO: This line of code loads data into the 'barDatabaseDataSet.Ingredients' table. You can move, or remove it, as needed.
            this.alcoholTableAdapter.Fill(this.barDatabaseDataSet.Alcohol);// This line of code loads data into the 'barDatabaseDataSet.Alcohol' table.
            this.otherTableAdapter.Fill(this.barDatabaseDataSet.Other);// This line of code loads data into the 'barDatabaseDataSet.Other' table.
            this.syrupTableAdapter.Fill(this.barDatabaseDataSet.Syrup);// This line of code loads data into the 'barDatabaseDataSet.Syrup' table.
            this.sodaTableAdapter.Fill(this.barDatabaseDataSet.Soda);// This line of code loads data into the 'barDatabaseDataSet.Soda' table.
            this.juiceTableAdapter.Fill(this.barDatabaseDataSet.Juice);// This line of code loads data into the 'barDatabaseDataSet.Juice' table.
            this.fruitTableAdapter.Fill(this.barDatabaseDataSet.Fruit);// This line of code loads data into the 'barDatabaseDataSet.Fruit' table.
            gridCondition(false);
            fillFlowLayoutPanel();
        }

        /*                   ACTIONS IN THE LEFT PANEL             */
        private void buttonAlcohol_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonAlcohol.Top;
            dataGridView.DataSource = alcoholBindingSource; // Устанавливаем источник данных
            alcoholBindingNavigator.BindingSource = alcoholBindingSource;// Устанавливаем меню действий (блок кнопок работы с таблицей)
            gridCondition(true);
            cocktailInfoCondition(false);
        }

        private void buttonSyrup_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonSyrup.Top;
            dataGridView.DataSource = syrupBindingSource;
            alcoholBindingNavigator.BindingSource = syrupBindingSource;
            gridCondition(true);
            cocktailInfoCondition(false);
        }

        private void buttonSoda_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonSoda.Top;
            dataGridView.DataSource = sodaBindingSource;
            alcoholBindingNavigator.BindingSource = sodaBindingSource;
            gridCondition(true);
            cocktailInfoCondition(false);
        }

        private void buttonJuice_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonJuice.Top;
            dataGridView.DataSource = juiceBindingSource;
            alcoholBindingNavigator.BindingSource = juiceBindingSource;
            gridCondition(true);
            cocktailInfoCondition(false);
        }

        private void buttonFruit_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonFruit.Top;
            dataGridView.DataSource = fruitBindingSource;
            alcoholBindingNavigator.BindingSource = fruitBindingSource;
            gridCondition(true);
            cocktailInfoCondition(false);

        }

        private void buttonOther_Click(object sender, EventArgs e)
        {
            panelLeft.Top = buttonOther.Top;
            dataGridView.DataSource = otherBindingSource;
            alcoholBindingNavigator.BindingSource = otherBindingSource;
            gridCondition(true);
            cocktailInfoCondition(false);
        }


        private void cocktailsDBButton_Click(object sender, EventArgs e)// выводим список коктелей которые можно создать из доступных ингридиентов 
        {
            gridCondition(false);
            cocktailInfoCondition(false);
            fillFlowLayoutPanel();
        }

        private void MakeButton_Click(object sender, EventArgs e)
        {

        }


        /*                  CONTROL APPLICATION                 */
        private void gridCondition(bool condition)   // Скрываем / Показываем таблицу с теми или иными ингридиентами 
        {
            alcoholBindingNavigator.Visible = condition;
            alcoholBindingNavigator.Enabled = condition;
            dataGridView.Visible = condition;
            dataGridView.Enabled = condition;
            panelLeft.Visible = condition;
            availableСocktailsPanel.Enabled = !condition;
            availableСocktailsPanel.Visible = !condition;
        }

        private void cocktailInfoCondition(bool condition)
        {
            cocktailInfo.Enabled = condition;
            cocktailInfo.Visible = condition;
        }



        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Validate();                                             // Сохраняем изменения
            this.alcoholBindingSource.EndEdit();                         // перед тем как 
            this.tableAdapterManager.UpdateAll(this.barDatabaseDataSet); // закрыть приложение
            Application.Exit();
        }

        private void minimazeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void fullScreenButton_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void alcoholBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.alcoholBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.barDatabaseDataSet);
        }

        /*                  SEARCH COCKTAILS              */
        private void fillFlowLayoutPanel()
        {
            availableСocktailsPanel.Controls.Clear();
            try
            {
                DataTable DrinksTable = this.barDatabaseDataSet.Drinks;// Локальная таблица от TheCoctailsApi которая содержит информацию о всех коктелях
                string link;
                WebClient wc = new WebClient();
                byte[] bytes;
                MemoryStream ms;
                getAllUserIngridients(); // собираем все ингридиенты из всех таблиц пользователя
                foreach (DataRow foundRows in DrinksTable.Select(getSearchString()))// выбираем из локальной таблицы коктели для которых у пользователя есть ингридиенты
                {
                    GroupBox groupBox = new GroupBox(); // создаем рамку с названием для фотки коктеля
                    groupBox.Name = foundRows["idDrink"].ToString(); // столбец idDrink содержит id коктеля
                    groupBox.Width = 240;
                    groupBox.Height = 265;
                    groupBox.Text = foundRows["strDrink"].ToString(); // столбец strDrink содержит название коктеля
                    groupBox.Font = new Font(this.Font, FontStyle.Bold);
                    groupBox.ForeColor = Color.FromArgb(255, 200, 0);
                    groupBox.Margin = new Padding(0, 0, 15, 20);

                    //drinkDescription1.Text = foundRows["strInstructions"].ToString();

                    link = foundRows["strDrinkThumb"].ToString(); // столбец strDrinkThumb содержит фото коктеля
                    bytes = wc.DownloadData(link);
                    ms = new MemoryStream(bytes);

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Width = groupBox.Width;
                    pictureBox.Height = groupBox.Height - 20;
                    pictureBox.Image = Image.FromStream(ms);
                    pictureBox.Dock = DockStyle.Bottom;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Click += new EventHandler(certainCocktailClick);
                    ms.Close();
                    groupBox.Controls.Add(pictureBox);
                    availableСocktailsPanel.Controls.Add(groupBox);
                    //return;
                }
                

            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }

        private string getSearchString()//("strIngredient IS NULL OR strIngredient IN ('', '', '')) AND"
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

        private void getAllUserIngridients() // Вызывает функцию заполнения списка ингредиентов пользователя
        {
            collectDataFromTable(this.barDatabaseDataSet.Alcohol);
            collectDataFromTable(this.barDatabaseDataSet.Syrup);
            collectDataFromTable(this.barDatabaseDataSet.Soda);
            collectDataFromTable(this.barDatabaseDataSet.Juice);
            collectDataFromTable(this.barDatabaseDataSet.Fruit);
            collectDataFromTable(this.barDatabaseDataSet.Other);
        }

        private void collectDataFromTable(DataTable table) // Заполняет список ингредиентами из таблиц
        {
            foreach (DataRow row in table.Rows)
            {
                userIngredients.Add(row["Name"].ToString());
            }
        }
 
        private void certainCocktailClick(object sender, EventArgs e)
        {
            requiredIngredientsPanel.Controls.Clear();

            availableСocktailsPanel.Enabled = false;
            availableСocktailsPanel.Visible = false;
            cocktailInfoCondition(true);

            DataTable IngredientsTable = this.barDatabaseDataSet.Ingredients; // Локальная таблица от TheCoctailsApi которая содержит информацию о всех коктелях
            DataRow ingredientsInfo;
            GroupBox clickedGroupBox = (GroupBox)((PictureBox)sender).Parent; // получаем нажатый GroupBox в GroupBox.Name лежит id коктеля
            DataTable DrinksTable = this.barDatabaseDataSet.Drinks; // Локальная таблица от TheCoctailsApi которая содержит информацию о всех коктелях
            DataRow coctailInfo = DrinksTable.Select("idDrink = " + clickedGroupBox.Name.ToString())[0]; // получаем информацию о коктеле по id
            cocktailName.Text = coctailInfo["strDrink"].ToString();
            cocktailInstruction.Text = coctailInfo["strInstructions"].ToString();
            cocktailGlass.Text = coctailInfo["strGlass"].ToString();
            //cocktailTagsTextBox.Text = coctailInfo["strTags"] + " | " + coctailInfo["strCategory"] + " | " + coctailInfo["strIBA"] + " | " + coctailInfo["strAlcoholic"];

            string link = coctailInfo["strDrinkThumb"].ToString(); // столбец strDrinkThumb содержит фото коктеля
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(link);
            MemoryStream ms = new MemoryStream(bytes);
            cocktailPictureBox.Image = Image.FromStream(ms);
            cocktailPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            ms.Close();

            string ingredientName;
            string ingredientMeasure;
            for (int i = 1; i < 16; i++) {//всего 15 ингридентов
                ingredientName = coctailInfo["strIngredient" + i].ToString();
                ingredientMeasure = coctailInfo["strMeasure" + i].ToString();
                if (ingredientName != "") {// если есть ингридент создаем для него:
                    GroupBox groupBox = new GroupBox(); // создаем рамку с названием для ингридиента коктеля
                    groupBox.Width = 240;
                    groupBox.Height = 265;
                    groupBox.Text = ingredientMeasure + " " + ingredientName; // столбец strDrink содержит название коктеля
                    groupBox.Font = new Font(this.Font, FontStyle.Bold);
                    groupBox.ForeColor = Color.FromArgb(255, 200, 0);
                    groupBox.Margin = new Padding(0, 0, 15, 20);

                    ingredientsInfo = IngredientsTable.Select("strIngredient = '" + ingredientName +"'")[0]; // получаем информацию о ингридиенте
                    link = ingredientsInfo["strIngredientThumb"].ToString(); // получаем ссылку на фото ингридиента
                    bytes = wc.DownloadData(link);
                    ms = new MemoryStream(bytes);

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Width = groupBox.Width;
                    pictureBox.Height = groupBox.Height - 20;
                    pictureBox.Image = Image.FromStream(ms);
                    pictureBox.Dock = DockStyle.Bottom;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    ms.Close();
                    groupBox.Controls.Add(pictureBox);
                    requiredIngredientsPanel.Controls.Add(groupBox);
                }
            }
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

        private const int cGrip = 16;// Grip size

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)//  this message use it's Result property to return the position of the cursor hot sopt. 
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos); //Computes the location of the specified screen point into client coordinates.
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}

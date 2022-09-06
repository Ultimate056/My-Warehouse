using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Sklad
{
    public partial class Form1 : Form
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SkladConnection"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        static SqlConnection connection = new SqlConnection(connectionString);
        DataTable dt;
        string NameSelectedID;
        string[] Manufacterer = { "AMD", "Intel", "Nvidia Production", "Kingston", "HyperX", "Hynix" };

        string[] TypeMemoryForGPU = { "GDDR4", "GDDR5", "GDDR6", "GDDR6X" };

        string[] TypeMemoryForRAM = { "DDR3", "DDR3L", "DDR4"};

        string[] FormFactors = { "DIMM", "SODIMM" };

        CoreMethods cm = new CoreMethods();
        public Form1()
        {
            InitializeComponent();
            SelectedTable.Items.AddRange(cm.GetAllNamesField());
            InitializeDataTable("GraphicCard", GraphicCardDataGrid);
            InitializeDataTable("CPU", CPUDataGrid);
            InitializeDataTable("RAM", RAMDataGrid);
            InitializeDataTable("Client", ClientsDataGrid);
            InitializeDataTable("OrdersBuy", OrdersBuyDataGrid);
            InitializeDataTable("OrdersSell", OrdersSellDataGrid);
            InitializeDataTable("Seller", SellersDataGrid);
            T1Man.DataSource = Manufacterer;
            T2Man.DataSource = Manufacterer;
            T3Man.DataSource = Manufacterer;
            T1TypeM.DataSource = TypeMemoryForGPU;
            T3TypeM.DataSource = TypeMemoryForRAM;
            T3FormFactor.DataSource = FormFactors;
        }

        // *Methods
        
        // Инициализировать название айдишника таблицы
        private string GetNameID()
        {
            string result = "";
            switch (SelectedTable.SelectedIndex.ToString())
            {
                case "Client": result = "ID_Client"; break;
                case "CPU": result = "ID_CPU"; break;
                case "Employee": result = "ID_Emp"; break;
                case "OrderBuy": result = "ID_OB"; break;
                case "OrderSell": result = "ID_OS"; break;
                case "Posts": result = "ID_Post"; break;
                case "RAM": result = "ID_RAM"; break;
                case "Seller": result = "ID_Seller"; break;
                case "GraphicCard": result = "ID_GC"; break;
            }
            return result;
        }

        private void InitializeDataTable(string NameTable, DataGridView dgv)
        {
            if(connection.State == ConnectionState.Open)
            {
                adapter = new SqlDataAdapter("SELECT * FROM " + NameTable, connection);
                dt = new DataTable();
                adapter.Fill(dt);
                dgv.DataSource = dt;
                return;
            }
            connection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM " + NameTable, connection);
            dt = new DataTable();
            adapter.Fill(dt);
            dgv.DataSource = dt;
            connection.Close();
        }

        // При выборе таблицы выводим её на экран
        private void SelectedTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDataTable(SelectedTable.SelectedItem.ToString(), MainDataGrid);
            NameSelectedID = GetNameID();
        }



        #region РАЗДЕЛ ТОВАРОВ
        // --------- РАЗДЕЛ ТОВАРОВ ------------
        private int SelectedGC = -1;
        private int SelectedCPU = -1;
        private int SelectedRAM = -1;
        private void GraphicCardDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedGC = int.Parse(GraphicCardDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            T1Name.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            T1Man.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            T1TP.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            T1Year.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
            T1V.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
            T1TypeM.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
            T1Freq.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
            T1Price.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
            T1Am.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[9].Value.ToString();
            T1Num.Text = GraphicCardDataGrid.Rows[e.RowIndex].Cells[10].Value.ToString();
        }
        private void CPUDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedCPU = int.Parse(CPUDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            T2Name.Text = CPUDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            T2Man.Text = CPUDataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            T2TP.Text = CPUDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            T2Year.Text = CPUDataGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
            T2BaseFreq.Text = CPUDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
            T2TurboFreq.Text = CPUDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
            T2Cores.Text = CPUDataGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
            T2Flows.Text = CPUDataGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
            T2Price.Text = CPUDataGrid.Rows[e.RowIndex].Cells[9].Value.ToString();
            T2Am.Text = CPUDataGrid.Rows[e.RowIndex].Cells[10].Value.ToString();
            T2Num.Text = CPUDataGrid.Rows[e.RowIndex].Cells[11].Value.ToString();
        }
        private void RAMDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedRAM = int.Parse(RAMDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            T3Name.Text = RAMDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            T3Man.Text = RAMDataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            T3Year.Text = RAMDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            T3V.Text = RAMDataGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
            T3TypeM.Text = RAMDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
            T3Freq.Text = RAMDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
            T3FormFactor.Text = RAMDataGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
            string RGB = RAMDataGrid.Rows[e.RowIndex].Cells[8].Value.ToString().Trim();
            if (RGB.Equals("есть"))
                T3IsRGB.Checked = true;
            else
                T3IsRGB.Checked = false;
            string rad = RAMDataGrid.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
            if (rad.Equals("есть"))
                T3IsRad.Checked = true;
            else
                T3IsRad.Checked = false;

            T3Price.Text = RAMDataGrid.Rows[e.RowIndex].Cells[10].Value.ToString();
            T3Am.Text = RAMDataGrid.Rows[e.RowIndex].Cells[11].Value.ToString();
            T3Num.Text = RAMDataGrid.Rows[e.RowIndex].Cells[12].Value.ToString();
        }

        private void T1Add_Click(object sender, EventArgs e)
        {
            connection.Open();
            cmd = new SqlCommand("INSERT INTO GraphicCard(Название, Производитель, Техпроцесс, Год_релиза, Объём_памяти, Тип_памяти, Частота, Цена, Количество_на_складе, Номер_склада) " +
                "VALUES(@name, @man, @tech, @year, @v, @typem, @freq, @price, @am, @num)", connection);
            cmd.Parameters.AddWithValue("@name", T1Name.Text);
            cmd.Parameters.AddWithValue("@man", T1Man.Text);
            cmd.Parameters.AddWithValue("@tech", int.Parse(T1TP.Text));
            cmd.Parameters.AddWithValue("@year", int.Parse(T1Year.Text));
            cmd.Parameters.AddWithValue("@v", int.Parse(T1V.Text));
            cmd.Parameters.AddWithValue("@typem", T1TypeM.Text);
            cmd.Parameters.AddWithValue("@freq", int.Parse(T1Freq.Text));
            cmd.Parameters.AddWithValue("@price", int.Parse(T1Price.Text));
            cmd.Parameters.AddWithValue("@am", int.Parse(T1Am.Text));
            cmd.Parameters.AddWithValue("@num", int.Parse(T1Num.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
            InitializeDataTable("GraphicCard", GraphicCardDataGrid);
        }

        private void T1Change_Click(object sender, EventArgs e)
        {
            if (SelectedGC != -1)
            {
                connection.Open();
                cmd = new SqlCommand("UPDATE GraphicCard SET Название=@name , Производитель=@man , Техпроцесс=@tech , Год_релиза=@year , Объём_памяти=@v , Тип_памяти=@typem , Частота=@freq , Цена=@price , Количество_на_складе=@am, Номер_склада=@num WHERE ID_GC=@id", connection);
                cmd.Parameters.AddWithValue("@name", T1Name.Text);
                cmd.Parameters.AddWithValue("@man", T1Man.Text);
                cmd.Parameters.AddWithValue("@tech", int.Parse(T1TP.Text));
                cmd.Parameters.AddWithValue("@year", int.Parse(T1Year.Text));
                cmd.Parameters.AddWithValue("@v", int.Parse(T1V.Text));
                cmd.Parameters.AddWithValue("@typem", T1TypeM.Text);
                cmd.Parameters.AddWithValue("@freq", int.Parse(T1Freq.Text));
                cmd.Parameters.AddWithValue("@price", int.Parse(T1Price.Text));
                cmd.Parameters.AddWithValue("@am", int.Parse(T1Am.Text));
                cmd.Parameters.AddWithValue("@num", int.Parse(T1Num.Text));
                cmd.Parameters.AddWithValue("@id", SelectedGC);
                cmd.ExecuteNonQuery();
                connection.Close();
                InitializeDataTable("GraphicCard", GraphicCardDataGrid);
                MessageBox.Show("Товар обновлён");
            }
            else
                MessageBox.Show("Не выбран элемент");
        }

        private void T1Delete_Click(object sender, EventArgs e)
        {
            if(SelectedGC != 1)
            {
                connection.Open();
                cmd = new SqlCommand("DELETE GraphicCard WHERE ID_GC=" + SelectedGC, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                InitializeDataTable("GraphicCard", GraphicCardDataGrid);
                MessageBox.Show("Товар успешно удалён");
            }
            else
                MessageBox.Show("Не выбран элемент");
        }

        private void T2Add_Click(object sender, EventArgs e)
        {
            connection.Open();
            cmd = new SqlCommand("INSERT INTO CPU(Название, Производитель, Техпроцесс, Год_релиза, Базовая_частота, Турбо_частота, Количество_ядер, Количество_потоков, Цена, Количество_на_складе, Номер_склада) " +
                "VALUES(@name, @man, @tech, @year, @bfreq, @tfreq, @cores, @flows, @price, @am, @num)", connection);
            cmd.Parameters.AddWithValue("@name", T2Name.Text);
            cmd.Parameters.AddWithValue("@man", T2Man.Text);
            cmd.Parameters.AddWithValue("@tech", int.Parse(T2TP.Text));
            cmd.Parameters.AddWithValue("@year", int.Parse(T2Year.Text));
            cmd.Parameters.AddWithValue("@bfreq", Math.Round(float.Parse(T2BaseFreq.Text),1));
            cmd.Parameters.AddWithValue("@tfreq", Math.Round(float.Parse(T2TurboFreq.Text),1));
            cmd.Parameters.AddWithValue("@cores", int.Parse(T2Cores.Text));
            cmd.Parameters.AddWithValue("@flows", int.Parse(T2Flows.Text));
            cmd.Parameters.AddWithValue("@price", int.Parse(T2Price.Text));
            cmd.Parameters.AddWithValue("@am", int.Parse(T2Am.Text));
            cmd.Parameters.AddWithValue("@num", int.Parse(T2Num.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
            InitializeDataTable("CPU", CPUDataGrid);
        }

        private void T2Change_Click(object sender, EventArgs e)
        {
            if (SelectedCPU != -1)
            {
                connection.Open();
                cmd = new SqlCommand("UPDATE CPU SET Название=@name , Производитель=@man , Техпроцесс=@tech , Год_релиза=@year , Базовая_частота=@bfreq , Турбо_частота=@tfreq , Количество_ядер=@cores , Количество_потоков=@flows, Цена=@price , Количество_на_складе=@am, Номер_склада=@num WHERE ID_CPU=@id", connection);
                cmd.Parameters.AddWithValue("@name", T2Name.Text);
                cmd.Parameters.AddWithValue("@man", T2Man.Text);
                cmd.Parameters.AddWithValue("@tech", int.Parse(T2TP.Text));
                cmd.Parameters.AddWithValue("@year", int.Parse(T2Year.Text));
                cmd.Parameters.AddWithValue("@bfreq", Math.Round(float.Parse(T2BaseFreq.Text), 1));
                cmd.Parameters.AddWithValue("@tfreq", Math.Round(float.Parse(T2TurboFreq.Text), 1));
                cmd.Parameters.AddWithValue("@cores", int.Parse(T2Cores.Text));
                cmd.Parameters.AddWithValue("@flows", int.Parse(T2Flows.Text));
                cmd.Parameters.AddWithValue("@price", int.Parse(T2Price.Text));
                cmd.Parameters.AddWithValue("@am", int.Parse(T2Am.Text));
                cmd.Parameters.AddWithValue("@num", int.Parse(T2Num.Text));
                cmd.Parameters.AddWithValue("@id", SelectedCPU);
                cmd.ExecuteNonQuery();
                connection.Close();
                InitializeDataTable("CPU", CPUDataGrid);
                MessageBox.Show("Товар обновлён");
            }
            else
                MessageBox.Show("Не выбран элемент");
        }

        private void T2Delete_Click(object sender, EventArgs e)
        {
            if (SelectedCPU != 1)
            {
                connection.Open();
                cmd = new SqlCommand("DELETE CPU WHERE ID_CPU=" + SelectedCPU, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                InitializeDataTable("CPU", CPUDataGrid);
                MessageBox.Show("Товар успешно удалён");
            }
            else
                MessageBox.Show("Не выбран элемент");
        }

        private void T3Add_Click(object sender, EventArgs e)
        {
            connection.Open();
            cmd = new SqlCommand("INSERT INTO RAM(Название, Производитель, Год_релиза, Объём_памяти, Тип_памяти, Частота, Формфактор, Подсветка, Радиатор, Цена, Количество_на_складе, Номер_склада) " +
                "VALUES(@name, @man, @year, @v, @typem, @freq,@form, @rgb, @rad, @price, @am, @num)", connection);
            cmd.Parameters.AddWithValue("@name", T3Name.Text);
            cmd.Parameters.AddWithValue("@man", T3Man.Text);
            cmd.Parameters.AddWithValue("@year", int.Parse(T3Year.Text));
            cmd.Parameters.AddWithValue("@v", int.Parse(T3V.Text));
            cmd.Parameters.AddWithValue("@typem", T3TypeM.Text);
            cmd.Parameters.AddWithValue("@freq", int.Parse(T3Freq.Text));
            cmd.Parameters.AddWithValue("@form", T3FormFactor.Text);

            string s1 = "";
            string s2 = "";
            if (T3IsRGB.Checked == true)
                s1 = "есть";
            else
                s1 = "нет";
            if (T3IsRad.Checked == true)
                s2 = "есть";
            else
                s2 = "нет";

            cmd.Parameters.AddWithValue("@rgb", s1);
            cmd.Parameters.AddWithValue("@rad", s2);

            cmd.Parameters.AddWithValue("@price", int.Parse(T3Price.Text));
            cmd.Parameters.AddWithValue("@am", int.Parse(T3Am.Text));
            cmd.Parameters.AddWithValue("@num", int.Parse(T3Num.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
            InitializeDataTable("RAM", RAMDataGrid);
        }

        private void T3Change_Click(object sender, EventArgs e)
        {
            if (SelectedRAM != -1)
            {
                connection.Open();
                cmd = new SqlCommand("UPDATE RAM SET Название=@name , Производитель=@man , Год_релиза=@year , Объём_памяти=@v , Тип_памяти=@typem , Частота=@freq , Формфактор=@form , Подсветка=@rgb, Радиатор=@rad, Цена=@price , Количество_на_складе=@am, Номер_склада=@num WHERE ID_RAM=@id", connection);
                cmd.Parameters.AddWithValue("@name", T3Name.Text);
                cmd.Parameters.AddWithValue("@man", T3Man.Text);
                cmd.Parameters.AddWithValue("@year", int.Parse(T3Year.Text));
                cmd.Parameters.AddWithValue("@v", int.Parse(T3V.Text));
                cmd.Parameters.AddWithValue("@typem", T3TypeM.Text);
                cmd.Parameters.AddWithValue("@freq", int.Parse(T3Freq.Text));
                cmd.Parameters.AddWithValue("@form", T3FormFactor.Text);

                string s1 = "";
                string s2 = "";
                if (T3IsRGB.Checked == true)
                    s1 = "есть";
                else
                    s1 = "нет";
                if (T3IsRad.Checked == true)
                    s2 = "есть";
                else
                    s2 = "нет";

                cmd.Parameters.AddWithValue("@rgb", s1);
                cmd.Parameters.AddWithValue("@rad", s2);

                cmd.Parameters.AddWithValue("@price", int.Parse(T3Price.Text));
                cmd.Parameters.AddWithValue("@am", int.Parse(T3Am.Text));
                cmd.Parameters.AddWithValue("@num", int.Parse(T3Num.Text));
                cmd.Parameters.AddWithValue("@id", SelectedRAM);

                cmd.ExecuteNonQuery();
                connection.Close();
                InitializeDataTable("RAM", RAMDataGrid);
                MessageBox.Show("Товар обновлён");
            }
            else
                MessageBox.Show("Не выбран элемент");
        }

        private void T3Delete_Click(object sender, EventArgs e)
        {
            if (SelectedRAM != 1)
            {
                connection.Open();
                cmd = new SqlCommand("DELETE RAM WHERE ID_RAM=" + SelectedRAM, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                InitializeDataTable("RAM", RAMDataGrid);
                MessageBox.Show("Товар успешно удалён");
            }
            else
                MessageBox.Show("Не выбран элемент");
        }


        private void ViewProductsButton_Click(object sender, EventArgs e)
        {
            OrdersBuyView.Hide();
            ProductsView.Show();
            tabControl1.SelectedIndex = 1;
        }

        #endregion


        private bool ExecuteAndCheckIsThereNumber(SqlCommand cmd)
        {
            object b = cmd.ExecuteScalar();
            if (cmd.ExecuteScalar() == null || cmd.ExecuteScalar() =="" || cmd.ExecuteScalar().ToString()=="-1")
            {
                MessageBox.Show("Такого объекта нет");
                return false;
            }
            string res = cmd.ExecuteScalar().ToString();
            bool checking = !(res == null);
            if (!checking)
                MessageBox.Show("Такого объекта нет");
            return checking;
        }



        #region РАЗДЕЛ ЗАКУПОК
        // ----------- ЗАКУПКИ -----------------
        private void AddOrderBuy_Click(object sender, EventArgs e)
        {
            if(NumberClientOB.Text=="" || NumberProductOB.Text == "" || CountProductOrderBuy.Text=="" || SumOrderBuy.Text=="")
            {
                MessageBox.Show("Заполните поля!");
                return;
            }
            bool checking = true;
            connection.Open();
            int NumberClient = int.Parse(NumberClientOB.Text);
            cmd = new SqlCommand("SELECT ID_Client FROM Client WHERE ID_Client=" + NumberClient, connection);
            checking = ExecuteAndCheckIsThereNumber(cmd);
            if (!checking)
                return;
            int num = int.Parse(NumberProductOB.Text);
            int CountProductOB = int.Parse(CountProductOrderBuy.Text);
            int price = 0;
            if(num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("SELECT ID_CPU FROM CPU WHERE ID_CPU=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM CPU WHERE ID_CPU=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new SqlCommand("SELECT Количество_на_складе FROM CPU WHERE ID_CPU=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                checking = !(CountProductOB > count || CountProductOB <= 0 || count == 0);
                if (!checking)
                {
                    MessageBox.Show("Ошибочный ввод в количестве");
                    return;
                }
            }
            if (num >= 100 && num <=199)
            {
                cmd = new SqlCommand("SELECT ID_GC FROM GraphicCard WHERE ID_GC=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM GraphicCard WHERE ID_GC=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new SqlCommand("SELECT Количество_на_складе FROM GraphicCard WHERE ID_GC=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                checking = !(CountProductOB > count || CountProductOB <= 0 || count == 0);
                if (!checking)
                {
                    MessageBox.Show("Ошибочный ввод в количестве");
                    return;
                }
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("SELECT ID_RAM FROM RAM WHERE ID_RAM=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM RAM WHERE ID_RAM=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new SqlCommand("SELECT Количество_на_складе FROM RAM WHERE ID_RAM=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                checking = !(CountProductOB > count || CountProductOB <= 0 || count == 0);
                if (!checking)
                {
                    MessageBox.Show("Ошибочный ввод в количестве");
                    return;
                }
            }
            cmd = new SqlCommand("INSERT INTO OrdersBuy(TotalSum,DateOfBuy,CountProduct,ID_Tovar, ID_Client) VALUES" +
                "(@sum, @date, @count, @idTovar, @idClient)", connection);
            cmd.Parameters.AddWithValue("sum", int.Parse(SumOrderBuy.Text));
            cmd.Parameters.AddWithValue("date", dateTimePicker1.Value.ToString());
            cmd.Parameters.AddWithValue("count", CountProductOB);
            cmd.Parameters.AddWithValue("idTovar", num);
            cmd.Parameters.AddWithValue("idClient", int.Parse(NumberClientOB.Text));
            cmd.ExecuteNonQuery();

            // Обновление числа
            if(num >=1 && num <=99)
            {
                cmd = new SqlCommand("UPDATE CPU SET Количество_на_складе=Количество_на_складе - " + CountProductOB + " WHERE ID_CPU=" + num, connection);
                cmd.ExecuteNonQuery();
                InitializeDataTable("CPU", CPUDataGrid);
            }
            if (num >= 100 && num <= 199)
            {
                cmd = new SqlCommand("UPDATE GraphicCard SET Количество_на_складе=Количество_на_складе - " + CountProductOB + " WHERE ID_GC=" + num, connection);
                cmd.ExecuteNonQuery();
                InitializeDataTable("GraphicCard", GraphicCardDataGrid);
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("UPDATE RAM SET Количество_на_складе=Количество_на_складе - " + CountProductOB + " WHERE ID_RAM=" + num, connection);
                cmd.ExecuteNonQuery();
                InitializeDataTable("RAM", RAMDataGrid);
            }
            InitializeDataTable("OrdersBuy", OrdersBuyDataGrid);
            MessageBox.Show("Накладная по закупке добавлена");
            CleanDataOrderBuy();
            connection.Close();
        }

        private int FillPrice()
        {
            if (NumberProductOB.Text == "" || CountProductOrderBuy.Text == "")
            {
                MessageBox.Show("Заполните поля!");
                return -1;
            }
            if(connection.State != ConnectionState.Open)
                connection.Open();
            int num = int.Parse(NumberProductOB.Text);
            bool checking;
            int price = 0;
            if (num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("SELECT ID_CPU FROM CPU WHERE ID_CPU=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return -1;
                cmd = new SqlCommand("SELECT Цена FROM CPU WHERE ID_CPU=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());
            }
            if (num >= 100 && num <= 199)
            {

                cmd = new SqlCommand("SELECT ID_GC FROM GraphicCard WHERE ID_GC=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return -1;
                cmd = new SqlCommand("SELECT Цена FROM GraphicCard WHERE ID_GC=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("SELECT ID_RAM FROM RAM WHERE ID_RAM=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return -1;
                cmd = new SqlCommand("SELECT Цена FROM RAM WHERE ID_RAM=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());
            }
            connection.Close();
            return price;
        }

        private void CountProductOrderBuy_TextChanged(object sender, EventArgs e)
        {
            int sum = FillPrice();
            if(sum != -1)
                SumOrderBuy.Text = Convert.ToString(int.Parse(CountProductOrderBuy.Text) * sum);
        }

        private  void CleanDataOrderBuy()
        {
            NumberClientOB.Text = "";
            NumberProductOB.Text = "";
            SumOrderBuy.Text = "";
            CountProductOrderBuy.Text = "";
            FreeCountProductOB.Text = "";
        }

        private void CalculateFreeCountProduct()
        {
            if (NumberProductOB.Text == "")
                return;
            connection.Open();
            int num = int.Parse(NumberProductOB.Text);
            bool checking;
            if (num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("SELECT ID_CPU FROM CPU WHERE ID_CPU=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                {
                    connection.Close();
                    return;
                }

                cmd = new SqlCommand("SELECT Количество_на_складе FROM CPU WHERE ID_CPU=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                FreeCountProductOB.Text = count.ToString();
            }
            if (num >= 100 && num <= 199)
            {
                cmd = new SqlCommand("SELECT ID_GC FROM GraphicCard WHERE ID_GC=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                {
                    connection.Close();
                    return;
                }
                cmd = new SqlCommand("SELECT Количество_на_складе FROM GraphicCard WHERE ID_GC=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                FreeCountProductOB.Text = count.ToString();
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("SELECT ID_RAM FROM RAM WHERE ID_RAM=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                {
                    connection.Close();
                    return;
                }
                cmd = new SqlCommand("SELECT Количество_на_складе FROM RAM WHERE ID_RAM=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                FreeCountProductOB.Text = count.ToString();
            }
            connection.Close();
        }

        // Сохранить номер продукта
        private void button1_Click(object sender, EventArgs e)
        {
            CalculateFreeCountProduct();
        }
        #endregion

        // ---------- ПОСТАВКИ -------------
        #region РАЗДЕЛ ПОСТАВОК
        private void SaveNumberProductOS_Click(object sender, EventArgs e)
        {
            if (NumberProductOS.Text == "")
                return;
            connection.Open();
            int num = int.Parse(NumberProductOS.Text);
            bool checking;
            if (num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("SELECT ID_CPU FROM CPU WHERE ID_CPU=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                {
                    connection.Close();
                    return;
                }

                cmd = new SqlCommand("SELECT Количество_на_складе FROM CPU WHERE ID_CPU=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                FreeCountProductOS.Text = count.ToString();
            }
            if (num >= 100 && num <= 199)
            {
                cmd = new SqlCommand("SELECT ID_GC FROM GraphicCard WHERE ID_GC=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                {
                    connection.Close();
                    return;
                }
                cmd = new SqlCommand("SELECT Количество_на_складе FROM GraphicCard WHERE ID_GC=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                FreeCountProductOS.Text = count.ToString();
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("SELECT ID_RAM FROM RAM WHERE ID_RAM=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                {
                    connection.Close();
                    return;
                }
                cmd = new SqlCommand("SELECT Количество_на_складе FROM RAM WHERE ID_RAM=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                FreeCountProductOS.Text = count.ToString();
            }
            connection.Close();
        }

        private void ViewProductsOS_Click(object sender, EventArgs e)
        {
            OrdersSellView.Hide();
            ProductsView.Show();
            tabControl1.SelectedIndex = 1;
        }

        private void AddOrderOfSell_Click(object sender, EventArgs e)
        {
            if (NumberSeller.Text == "" || NumberProductOS.Text == "" || CountProductOS.Text == "" || SumProductOS.Text == "")
            {
                MessageBox.Show("Заполните поля!");
                return;
            }
            bool checking = true;
            connection.Open();
            int NumberClient = int.Parse(NumberSeller.Text);
            cmd = new SqlCommand("SELECT ID_Seller FROM Seller WHERE ID_Seller=" + NumberClient, connection);
            checking = ExecuteAndCheckIsThereNumber(cmd);
            if (!checking)
                return;
            int num = int.Parse(NumberProductOS.Text);
            int countProductOS = int.Parse(CountProductOS.Text);
            int price = 0;
            if (num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("SELECT ID_CPU FROM CPU WHERE ID_CPU=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM CPU WHERE ID_CPU=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new SqlCommand("SELECT Количество_на_складе FROM CPU WHERE ID_CPU=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                checking = !(countProductOS <= 0);
                if (!checking)
                {
                    MessageBox.Show("Ошибочный ввод в количестве");
                    return;
                }
            }
            if (num >= 100 && num <= 199)
            {
                cmd = new SqlCommand("SELECT ID_GC FROM GraphicCard WHERE ID_GC=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM GraphicCard WHERE ID_GC=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new SqlCommand("SELECT Количество_на_складе FROM GraphicCard WHERE ID_GC=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                checking = !(countProductOS <= 0);
                if (!checking)
                {
                    MessageBox.Show("Ошибочный ввод в количестве");
                    return;
                }
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("SELECT ID_RAM FROM RAM WHERE ID_RAM=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM RAM WHERE ID_RAM=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new SqlCommand("SELECT Количество_на_складе FROM RAM WHERE ID_RAM=" + num, connection);
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                checking = !(countProductOS <= 0);
                if (!checking)
                {
                    MessageBox.Show("Ошибочный ввод в количестве");
                    return;
                }
            }
            cmd = new SqlCommand("INSERT INTO OrdersSell(TotalSum,DateOfBuy,CountProduct,ID_Tovar, ID_Seller) VALUES" +
                "(@sum, @date, @count, @idTovar, @idClient)", connection);
            cmd.Parameters.AddWithValue("sum", int.Parse(SumProductOS.Text));
            cmd.Parameters.AddWithValue("date", dateTimePicker2.Value.ToString());
            cmd.Parameters.AddWithValue("count", countProductOS);
            cmd.Parameters.AddWithValue("idTovar", num);
            cmd.Parameters.AddWithValue("idClient", int.Parse(NumberSeller.Text));
            cmd.ExecuteNonQuery();

            // Обновление числа
            if (num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("UPDATE CPU SET Количество_на_складе=Количество_на_складе + " + countProductOS + " WHERE ID_CPU=" + num, connection);
                cmd.ExecuteNonQuery();
                InitializeDataTable("CPU", CPUDataGrid);
            }
            if (num >= 100 && num <= 199)
            {
                cmd = new SqlCommand("UPDATE GraphicCard SET Количество_на_складе=Количество_на_складе + " + countProductOS + " WHERE ID_GC=" + num, connection);
                cmd.ExecuteNonQuery();
                InitializeDataTable("GraphicCard", GraphicCardDataGrid);
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("UPDATE RAM SET Количество_на_складе=Количество_на_складе + " + countProductOS + " WHERE ID_RAM=" + num, connection);
                cmd.ExecuteNonQuery();
                InitializeDataTable("RAM", RAMDataGrid);
            }
            InitializeDataTable("OrdersSell", OrdersSellDataGrid);
            MessageBox.Show("Накладная по поставке добавлена");
            CleanDataOrderSell();
            connection.Close();
        }

        private void CountProductOS_TextChanged(object sender, EventArgs e)
        {
            if (NumberProductOS.Text == "" || CountProductOS.Text == "")
            {
                MessageBox.Show("Заполните поля!");
                return;
            }
            if (connection.State != ConnectionState.Open)
                connection.Open();
            int num = int.Parse(NumberProductOS.Text);
            bool checking;
            int price = 0;
            if (num >= 1 && num <= 99)
            {
                cmd = new SqlCommand("SELECT ID_CPU FROM CPU WHERE ID_CPU=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM CPU WHERE ID_CPU=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());
            }
            if (num >= 100 && num <= 199)
            {

                cmd = new SqlCommand("SELECT ID_GC FROM GraphicCard WHERE ID_GC=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM GraphicCard WHERE ID_GC=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());
            }
            if (num >= 200 && num <= 299)
            {
                cmd = new SqlCommand("SELECT ID_RAM FROM RAM WHERE ID_RAM=" + num, connection);
                checking = ExecuteAndCheckIsThereNumber(cmd);
                if (!checking)
                    return;
                cmd = new SqlCommand("SELECT Цена FROM RAM WHERE ID_RAM=" + num, connection);
                price = int.Parse(cmd.ExecuteScalar().ToString());
            }
            connection.Close();
            SumProductOS.Text = Convert.ToString(int.Parse(CountProductOS.Text) * price);
        }

        private void CleanDataOrderSell()
        {
            NumberSeller.Text = "";
            NumberProductOS.Text = "";
            SumProductOS.Text = "";
            CountProductOS.Text = "";
            FreeCountProductOS.Text = "";
        }
        #endregion
    }
}

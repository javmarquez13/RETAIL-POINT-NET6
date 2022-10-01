using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Data.SqlClient;
using RETAIL_POINT_NET6.Clases;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace RETAIL_POINT_NET6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string GlobalUser = string.Empty;



        ObservableCollection<Product> _Product = new ObservableCollection<Product>();

        public MainWindow(string User)
        {
            InitializeComponent();
            InitializeDgvRetailPoint();
            //InitializeDgvInventory();

            GlobalUser = User;
            lblUser.Content = GlobalUser.Remove(1); 
            
            if(GlobalUser == "Operator") 
            {
                TabInventory.Visibility = Visibility.Hidden;
                TabInventory.IsEnabled = false;
                TabBarcodeGenerator.Visibility = Visibility.Hidden;
                TabBarcodeGenerator.IsEnabled = false;
                TabLicence.Visibility = Visibility.Hidden;
                TabLicence.IsEnabled = false;
            }
        }



        #region SQL FUNCTIONS

        SqlConnection connection = new SqlConnection();
        string cadena = @"Data Source=AORUSPRO\SQLEXPRESS;Initial Catalog=RetailDB; Integrated Security=True";

        void CloseSQLConnection()
        {
            try
            {
                connection.Close();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void OpenSQLConnection()
        {

            try
            {
                connection.ConnectionString = cadena;
                connection.Open();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void INSERT(string Id, string Name, string Description, double Price)
        {
            string _InsertSQL = "Insert into STOCK_TAKING ([Id] ,[Name] ,[Description] ,[Price]) " +
                                " values ('" + Id + "' ,'" + Name + "' ,'" + Description + "' ,'" + Price + "')";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = _InsertSQL;

            int result = command.ExecuteNonQuery();
        }

        void DELETE(string Id)
        {
            string _DeleteSQL = "Delete from STOCK_TAKING where Id = '" + Id + "'";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _DeleteSQL;
            int result = command.ExecuteNonQuery();
        }


        #endregion


        //void OnLayoutUpdated(object sender, EventArgs e)
        //{
        //    double countersMinWidth = CountersList.Columns[0].ActualWidth + CountersList.Columns[1].ActualWidth + CountersList.Columns[2].ActualWidth + CountersList.Margin.Left + CountersList.Margin.Right;
        //    double soundsMinWidth = SoundsList.Columns[0].ActualWidth + SoundsList.Columns[1].ActualWidth + SoundsList.Columns[2].ActualWidth + SoundsList.Margin.Left + SoundsList.Margin.Right;
        //    if (countersMinWidth < 205) countersMinWidth = 205;
        //    if (soundsMinWidth < 205) soundsMinWidth = 205;
        //    Grid.ColumnDefinitions[0].MinWidth = countersMinWidth;
        //    Grid.ColumnDefinitions[2].MinWidth = soundsMinWidth;
        //    this.MinWidth = countersMinWidth + soundsMinWidth + Splitter.ActualWidth + 18;
        //}


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseSQLConnection();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            { if (e.ChangedButton == MouseButton.Left) DragMove(); }
            catch (Exception) { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        #region SECTION RETAIL POIN 
        public struct QueryShapeRetailPoint
        {
            public string IdProduct { set; get; }
            public string NameProduct { set; get; }
            public string DescriptionProduct { set; get; }
            public string PriceProduct { set; get; }
            public string StockProduct { set; get; }
        }

        void InitializeDgvRetailPoint()
        {
            DataGridTextColumn IdProduct = new DataGridTextColumn();
            IdProduct.Header = "IdProduct";
            IdProduct.Binding = new Binding("IdProduct");
            IdProduct.Width = 120;
            IdProduct.IsReadOnly = true;

            DataGridTextColumn NameProduct = new DataGridTextColumn();
            NameProduct.Header = "NameProduct";
            NameProduct.Binding = new Binding("NameProduct");
            NameProduct.Width = 200;
            NameProduct.IsReadOnly = true;

            DataGridTextColumn DescriptionProduct = new DataGridTextColumn();
            DescriptionProduct.Header = "DescriptionProduct";
            DescriptionProduct.Binding = new Binding("DescriptionProduct");
            DescriptionProduct.Width = 330;
            DescriptionProduct.IsReadOnly = true;

            DataGridTextColumn PriceProduct = new DataGridTextColumn();
            PriceProduct.Header = "PriceProduct";
            PriceProduct.Binding = new Binding("PriceProduct");
            PriceProduct.Width = 100;
            PriceProduct.IsReadOnly = true;

            DataGridTextColumn StockProduct = new DataGridTextColumn();
            StockProduct.Header = "StockProduct";
            StockProduct.Binding = new Binding("StockProduct");
            StockProduct.Width = 100;
            StockProduct.IsReadOnly = true;

            dgRetailPoint.Columns.Add(IdProduct);
            dgRetailPoint.Columns.Add(NameProduct);
            dgRetailPoint.Columns.Add(DescriptionProduct);
            dgRetailPoint.Columns.Add(PriceProduct);
            dgRetailPoint.Columns.Add(StockProduct);
        }

        void WriteDgv(string From, string _IdProduct, string _NameProduct, string _DescriptionProduct, string _PriceProduct, string _StockProduct)
        {
            try
            {
                //if (From == "INVENTORY")
                //{
                //    dgStock.Items.Add(new QueryShapeInventory { IdProduct = _IdProduct, NameProduct = _NameProduct, DescriptionProduct = _DescriptionProduct, PriceProduct = _PriceProduct, StockProduct = _StockProduct });

                //    try
                //    {
                //        if (dgStock.Items.Count > 0)
                //        {
                //            var border = VisualTreeHelper.GetChild(dgStock, 0) as Decorator;
                //            if (border != null)
                //            {
                //                var scroll = border.Child as ScrollViewer;
                //                if (scroll != null) scroll.ScrollToEnd();
                //            }
                //        }

                //    }
                //    catch
                //    {

                //    }
                //}

                if (From == "RETAIL POINT")
                {
                    dgRetailPoint.Items.Add(new QueryShapeRetailPoint { IdProduct = _IdProduct, NameProduct = _NameProduct, DescriptionProduct = _DescriptionProduct, PriceProduct = _PriceProduct, StockProduct = _StockProduct });


                    if (dgRetailPoint.Items.Count > 0)
                    {
                        var border = VisualTreeHelper.GetChild(dgRetailPoint, 0) as Decorator;
                        if (border != null)
                        {
                            var scroll = border.Child as ScrollViewer;
                            if (scroll != null) scroll.ScrollToEnd();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("APP ERROR:" + ex.Message);
            }
        }

        private void txtProductID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tuple<int, DataTable> _result = SQLFunctions.Query(txtProductID.Text);
                DataTable _dt = _result.Item2;

                if (_dt.Rows.Count > 0)
                {
                    string id = _dt.Rows[0]["Id"] as string;
                    string Name = _dt.Rows[0]["Name"] as string;
                    string Description = _dt.Rows[0]["Description"] as string;
                    decimal Price = Convert.ToDecimal(_dt.Rows[0]["Price"]);
                    int Stock = Convert.ToInt32(_dt.Rows[0]["Stock"]);

                    WriteDgv("RETAIL POINT", id, Name, Description, Price.ToString(), Stock.ToString());
                }
                else { MessageBox.Show("Product not found into Data Base"); }


                txtProductID.Clear();
            }
        }
        #endregion

        #region SECTION INVENTORY
        private static Label FindClickedItem(object sender)
        {
            var mi = sender as MenuItem;
            if (mi == null)
            {
                return null;
            }

            var cm = mi.CommandParameter as ContextMenu;
            if (cm == null)
            {
                return null;
            }

            return cm.PlacementTarget as Label;
        }

        private void View_OnClick(object sender, RoutedEventArgs e)
        {
            var clickedItem = FindClickedItem(sender);
            if (clickedItem != null)
            {
                MessageBox.Show(" Viewing: " + clickedItem.Content);
            }
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var clickedItem = FindClickedItem(sender);
            if (clickedItem != null)
            {
                string newName = "Connection edited on " + DateTime.Now.ToLongTimeString();
                string oldName = Convert.ToString(clickedItem.Content);
                clickedItem.Content = newName;
                MessageBox.Show(string.Format("Changed name from '{0}' to '{1}'", oldName, newName));
            }
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var clickedItem = FindClickedItem(sender);
            if (clickedItem != null)
            {
                string oldName = Convert.ToString(clickedItem.Content);
                //listConnections.Children.Remove(clickedItem);
                MessageBox.Show(string.Format("Removed '{0}'", oldName));
            }
        }



        void InitializeDgvInventory()
        {
            DataGridTextColumn IdProduct = new DataGridTextColumn();
            IdProduct.Header = "IdProduct";
            IdProduct.Binding = new Binding("IdProduct");
            IdProduct.Width = 120;
            IdProduct.IsReadOnly = true;

            DataGridTextColumn NameProduct = new DataGridTextColumn();
            NameProduct.Header = "NameProduct";
            NameProduct.Binding = new Binding("NameProduct");
            NameProduct.Width = 200;
            NameProduct.IsReadOnly = true;

            DataGridTextColumn DescriptionProduct = new DataGridTextColumn();
            DescriptionProduct.Header = "DescriptionProduct";
            DescriptionProduct.Binding = new Binding("DescriptionProduct");
            DescriptionProduct.Width = 330;
            DescriptionProduct.IsReadOnly = true;

            DataGridTextColumn PriceProduct = new DataGridTextColumn();
            PriceProduct.Header = "PriceProduct";
            PriceProduct.Binding = new Binding("PriceProduct");
            PriceProduct.Width = 100;
            PriceProduct.IsReadOnly = true;

            DataGridTextColumn StockProduct = new DataGridTextColumn();
            StockProduct.Header = "StockProduct";
            StockProduct.Binding = new Binding("StockProduct");
            StockProduct.IsReadOnly = true;

            //dgStock.Columns.Add(IdProduct);
            //dgStock.Columns.Add(NameProduct);
            //dgStock.Columns.Add(DescriptionProduct);
            //dgStock.Columns.Add(PriceProduct);
            //dgStock.Columns.Add(StockProduct);
        }

        bool EnviromentInventory = false;

        public struct QueryShapeInventory
        {
            public string IdProduct { set; get; }
            public string NameProduct { set; get; }
            public string DescriptionProduct { set; get; }
            public string PriceProduct { set; get; }
            public string StockProduct { set; get; }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _converter = new BrushConverter();
            var tab = sender as TabControl;

            if (tab != null)
            {
                TabItem item = (TabItem)tab.SelectedItem;

                if (item.Header.ToString() != "          INVENTORY          ") EnviromentInventory = false;

                if (item.Header.ToString().Contains("INVENTORY") && !EnviromentInventory)
                {
                    EnviromentInventory = true;
                    Tuple<int, DataTable> _result = SQLFunctions.QueryAllStock();
                    DataTable _dt = _result.Item2;

                    dgStockN.Items.Clear();
                    dgStockN.Items.Refresh();

                    foreach (DataRow row in _dt.Rows)
                    {

                        string id = row["Id"] as string;
                        string Name = row["Name"] as string;
                        string Description = row["Description"] as string;
                        decimal Price = Convert.ToDecimal(row["Price"]);
                        int Stock = Convert.ToInt32(row["Stock"]);


                        _Product.Add(new Product { Id = id, Character=Name.Substring(0,1), Name = Name, Description = Description, Price = Price, Stock = Stock, BgColor = (Brush)_converter.ConvertFromString("#1098ad") });

                        WriteDgv("INVENTORY", id, Name, Description, Price.ToString(), Stock.ToString());
                    }


                    dgStockN.ItemsSource = _Product;
                }
            }
        }

        private void dgStock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ADDING NEW DATAGRID DESIGN
            //if (sender != null)
            //{
            //    DataGrid grid = sender as DataGrid;
            //    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            //    {
            //        DataGridRow dgr = dgStock.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;

            //    }
            //}
        }

        private void txtAddProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(cBoxSelectOperation.Text))
            {
                if (cBoxSelectOperation.Text == "Add Product Stock") AddProductStock(txtAddProduct.Text);

                if (cBoxSelectOperation.Text == "Delete Product Stock") DeleteProductStock(txtAddProduct.Text);

                if (cBoxSelectOperation.Text == "Register new Product") RegisterNewProduct(txtAddProduct.Text);

                if (cBoxSelectOperation.Text == "Delete old Product") DeleteOldProduct(txtAddProduct.Text);
            }
        }


        void AddProductStock(string IdProduct)
        {
            Tuple<int, DataTable> Items = SQLFunctions.Query(IdProduct);

            if (Items.Item2.Rows.Count > 0)
            {
                int currentStock = Convert.ToInt32(Items.Item2.Rows[0]["Stock"]);
                SQLFunctions.UpdateStock(txtAddProduct.Text, currentStock + 1);

                Tuple<int, DataTable> _result = SQLFunctions.QueryAllStock();
                DataTable _dt = _result.Item2;

                dgStockN.Items.Clear();
                dgStockN.Items.Refresh();

                foreach (DataRow row in _dt.Rows)
                {

                    string id = row["Id"] as string;
                    string Name = row["Name"] as string;
                    string Description = row["Description"] as string;
                    decimal Price = Convert.ToDecimal(row["Price"]);
                    int Stock = Convert.ToInt32(row["Stock"]);

                    WriteDgv("INVENTORY", id, Name, Description, Price.ToString(), Stock.ToString());
                }
            }
            if (Items.Item2.Rows.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Articulo no encontrado \n\n Deseas registrar articulo con Id:" + IdProduct, "", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    new GUI.RegisterProduct(IdProduct).ShowDialog();
                }
            }
        }

        void DeleteProductStock(string IdProduct)
        {
            Tuple<int, DataTable> Items = SQLFunctions.Query(IdProduct);

            if (Items.Item2.Rows.Count > 0)
            {
                int currentStock = Convert.ToInt32(Items.Item2.Rows[0]["Stock"]);
                SQLFunctions.UpdateStock(txtAddProduct.Text, currentStock - 1);

                Tuple<int, DataTable> _result = SQLFunctions.QueryAllStock();
                DataTable _dt = _result.Item2;

                dgStockN.Items.Clear();
                dgStockN.Items.Refresh();

                foreach (DataRow row in _dt.Rows)
                {
                    string id = row["Id"] as string;
                    string Name = row["Name"] as string;
                    string Description = row["Description"] as string;
                    decimal Price = Convert.ToDecimal(row["Price"]);
                    int Stock = Convert.ToInt32(row["Stock"]);

                    WriteDgv("INVENTORY", id, Name, Description, Price.ToString(), Stock.ToString());
                }
            }
            if (Items.Item2.Rows.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Articulo no encontrado Id:" + IdProduct, "", MessageBoxButton.OK);
            }

        }

        void RegisterNewProduct(string IdProduct)
        {
            Tuple<int, DataTable> Items = SQLFunctions.Query(IdProduct);

            if (Items.Item2.Rows.Count == 0) 
            {
                new GUI.RegisterProduct(IdProduct).ShowDialog();
            }
            else 
            {
                MessageBox.Show("Product Already Registered");
            }
            
        }

        void DeleteOldProduct(string IdProduct) 
        {
            Tuple<int, DataTable> Items = SQLFunctions.Query(IdProduct);

            if (Items.Item2.Rows.Count > 0)
            {
                int result = SQLFunctions.DeleteProduct(IdProduct);
            }
            else
            {
                MessageBox.Show("Product Not Found");
            }          
        }





        #endregion

        #region SECTION BARCODE GENERATOR
        private void txtIdProductBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();       
                b.IncludeLabel = true;
                System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.CODE128C, txtIdProductBarCode.Text, System.Drawing.Color.Black, System.Drawing.Color.White,290,120);

                ImageSource imgSource = ConvertImage(img);

                imgBarCode.Source = imgSource;

            }
        }



        public static ImageSource ConvertImage(System.Drawing.Image image)
        {
            try
            {
                if (image != null)
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch { }
            return null;
        }


        #endregion








        #region SECTION PLACE HOLDER TEXT BOXS
        private void txtProductID_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(sender, e);
        }

        private void txtProductID_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(sender, e);
        }

        void RemoveText(object sender, EventArgs e)
        {
            if (txtProductID.Text == "Escanea Product ID..." || txtAddProduct.Text == "Escanea Product ID...")
            {
                txtProductID.Text = "";
                txtAddProduct.Text = "";
            }

        }

        void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                txtProductID.Text = "Escanea Product ID...";
                txtAddProduct.Text = "Escanea Product ID...";
            }

        }
        #endregion


        private void dgRetailPoint_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void dgRetailPoint_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgStock_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            QueryShapeInventory shape = new QueryShapeInventory();
            shape = (QueryShapeInventory)row.DataContext;

            if(shape.IdProduct == txtAddProduct.Text) 
            {
                row.Background = new SolidColorBrush(Colors.Blue);
                row.Foreground = new SolidColorBrush(Colors.White);

                txtAddProduct.Clear();
                txtAddProduct.Focus();
            }
        }

        private void lblUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


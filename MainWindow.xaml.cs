using System;
using System.Collections.Generic;
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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CourseWork.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Serializable]
    public partial class MainWindow : Window
    {

        static private bool isSaved = false; // блокировка на открытие из файла, если он еще не сохранялся.
        private int current = -1; // указатель на магазин, с которым проводится работа. Диапазон присваивается от 1 - по ntw.Count
        private Journal jrn;
        private NewNetwork ntw;
        private bool error = false, new_price = false, new_quant = false; // флаги для ввода индекса выбираемого магазина, ввода новой цены и количества.
        private int _price = -1, _quantity = -1, update_num = -1;


        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void TestTextBox_TextChanged(object sender, TextChangedEventArgs e) // Окно для вывода коллекции
        {

        }

        private void Start()
        {

            Buttons_Start();
            ntw = new NewNetwork();
            ntw.Name = "Not_Fix_Price";
            jrn = new Journal();
            ntw.NetworkCountWasChanged += new NewNetwork.CollectionHandler(jrn.NetworkCountWasChanged);
            ntw.NetworkReferenceWasChanged += new NewNetwork.CollectionHandler(jrn.NetworkReferenceWasChanged);
            ntw.StoreCountWasChanged += new NewNetwork.CollectionHandler(jrn.StoreCountWasChanged);
            ntw.StoreObjectWasUpdated += new NewNetwork.CollectionHandler(jrn.StoreObjectWasUpdated);
            ntw.StoreReferenceWasChanged += new NewNetwork.CollectionHandler(jrn.StoreReferenceWasChanged);         
        }

        #region upd
        private void GetNewQuantity()
        {
            if (!error) { MessageBox.Show("Введите новое количество товара в поле ввода и нажмите кнопку 'Обновить.'"); error = true; }
            else
            {
                int x;
                if (Int32.TryParse(EnterField.Text, out x) == false || x < 0) MessageBox.Show("Неверное значение!", "Эй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    error = false;
                    new_quant = false;
                    new_price = true;
                    _quantity = x;
                    MessageBox.Show("Для ввода новой стоимости нажмите на кнопку 'Обновить'!");
                }
            }
        }

        private void GetNewPrice()
        {
            if (!error) { MessageBox.Show("Введите новую стоимость товара в поле ввода и нажмите кнопку 'Обновить'."); error = true; }
            else
            {
                int x;
                if (Int32.TryParse(EnterField.Text, out x) == false || x < 0) MessageBox.Show("Неверное значение!", "Эй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    error = false;
                    new_price = false;
                    _price = x;
                    try
                    {
                        ntw.Update(current - 1, update_num - 1, _quantity, _price);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка в обновлении товара!", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    update_num = -1;
                    MessageBox.Show("Товар обновлен!");

                }
            }
        }
        #endregion

        #region printers
        private void PrintNetwork(Network ntw)
        {
            if (ntw.Count == 0) {
                //foreach (var s in ntw)
                TestTextBox.Text = "";
                //MessageBox.Show("I WAS HERE!");
                MessageBox.Show("Пустая коллекция", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                BaseInfo(ntw.Name, ntw);
                TestTextBox.Text = "'" + ntw.Name + "'\n";
                foreach (var s in ntw)
                {
                    TestTextBox.Text += "\n" + s.Name;
                    foreach (var f in s)
                        TestTextBox.Text += "\n" + f.ToString();
                }
            }
        } // выводит на экран сеть и базовую информацию.
        
        private void PrintJournal(Journal jrn)
        {
            try {
                if (jrn != null) JournalTextBox.Text = jrn.ToString();
            }
            catch
            {
                MessageBox.Show("Пустой журнал", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintStore()
        {
            if (ntw[current - 1].Count == 0) MessageBox.Show("Пустой магазин");
            else
            {
                TestTextBox.Text = "'" + ntw[current - 1].Name + "'";
                foreach (var s in ntw[current - 1])
                    TestTextBox.Text += "\n" + s.ToString();
            }
        }
        #endregion

        #region Journal_Menu
        private void Show_Journal_clicked(object sender, RoutedEventArgs e)
        {
            PrintJournal(jrn);
        }

        private void Save_Journal(object sender, RoutedEventArgs e)
        {
            try {
                Journal sf = jrn;
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream("journal.txt", FileMode.Create))
                {
                    bf.Serialize(fs, sf);
                    MessageBox.Show("Журнал сохранен.");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе журнала", "Хэй, очнись!",
         MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Buttons_switch
        private void Buttons_Start()
        {
            Add.Visibility = Visibility.Collapsed; //Добавить Товар
            Remove.Visibility = Visibility.Collapsed;//Удалить товар
            Update.Visibility = Visibility.Collapsed;//Обновить товар
            Read.Visibility = Visibility.Collapsed;//Вывести список товаров текущего магазина

            Industrial.Visibility = Visibility.Collapsed;// Показывает только производственные товары
            Food.Visibility = Visibility.Collapsed;//Показывает только продовольственные товары

            FindByPrice.Visibility = Visibility.Collapsed;//Выполняет поисковый запрос по введенной стоимости
            FindByQuantity.Visibility = Visibility.Collapsed;//Выполняет поисковый запрос по введенному количеству
            FindByName.Visibility = Visibility.Collapsed;//Выполняет поисковый запрос по введенному названию товара

            AddStore.Visibility = Visibility.Collapsed; //Добавляет магазин
            DeleteStore.Visibility = Visibility.Collapsed;//Удаляет магазин по индексу
            ClearNetwork.Visibility = Visibility.Collapsed;// Очищает базовую коллекцию
            ShowNetwork.Visibility = Visibility.Collapsed;// Выводит всю сеть
            ChooseStore.Visibility = Visibility.Collapsed;// Выбор магазина для дальнейшей работы

            Download.Visibility = Visibility.Collapsed; // Скачивает журнал
            ShowJournal.Visibility = Visibility.Collapsed; // Вывод журнал
        }

        private void Buttons_SelectStore()
        {
            Add.Visibility = Visibility.Collapsed;
            Remove.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Collapsed;
            Read.Visibility = Visibility.Collapsed;

            Industrial.Visibility = Visibility.Collapsed;
            Food.Visibility = Visibility.Collapsed;

            AddStore.Visibility = Visibility.Visible;
            DeleteStore.Visibility = Visibility.Visible;
            ClearNetwork.Visibility = Visibility.Visible;
            ShowNetwork.Visibility = Visibility.Visible;
            ChooseStore.Visibility = Visibility.Visible;

            FindByPrice.Visibility = Visibility.Collapsed;
            FindByQuantity.Visibility = Visibility.Collapsed;
            FindByName.Visibility = Visibility.Collapsed;

            Download.Visibility = Visibility.Visible;
            ShowJournal.Visibility = Visibility.Visible;
        }

        private void Buttons_Work()
        {
            Add.Visibility = Visibility.Visible;
            Remove.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Visible;
            Read.Visibility = Visibility.Visible;

            Industrial.Visibility = Visibility.Visible;
            Food.Visibility = Visibility.Visible;

            AddStore.Visibility = Visibility.Visible;
            DeleteStore.Visibility = Visibility.Visible;
            ClearNetwork.Visibility = Visibility.Visible;
            ShowNetwork.Visibility = Visibility.Visible;
            ChooseStore.Visibility = Visibility.Visible;

            FindByPrice.Visibility = Visibility.Visible;
            FindByQuantity.Visibility = Visibility.Visible;
            FindByName.Visibility = Visibility.Visible;

            Download.Visibility = Visibility.Visible;
            ShowJournal.Visibility = Visibility.Visible;
        }
        #endregion

        #region Store_Menu

        private void Store_Add(object sender, RoutedEventArgs e) // Добавление товара в текущий магазин
        {
            ntw.Add_inStore(current);
            MessageBox.Show("Товар добавлен!");
            CurrentStore(); //обновляет информацию после добавления
        }

        private void Store_Remove(object sender, RoutedEventArgs e) // Удаление товара из текущего магазина.
        {
            if (ntw[current - 1].Count == 0) MessageBox.Show("Пустой магазин!", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else {
                if (!error) { MessageBox.Show("Введите номер удаляемого элемента в поле ввода."); error = true; }
                else
                {


                    int num;
                    if (Int32.TryParse(EnterField.Text, out num) == false || (num < 1) || (num > ntw[current - 1].Count)) MessageBox.Show("Индекс вне диапазона!", "Эй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        error = false;
                        MessageBox.Show("Товар удален!");
                        ntw.Remove(current, num);
                        CurrentStore();
                    }

                }
            }
        }

        private void Store_Update(object sender, RoutedEventArgs e) // сделать ввод нового количества и цены через новое окошко
        {
            if (ntw[current - 1].Count == 0) MessageBox.Show("Пустой магазин", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else {                
                if (!new_price && !new_quant)
                {
                    if (!error) { MessageBox.Show("Введите номер обновляемого элемента в поле ввода."); error = true; }
                    else
                    {
                        int num;
                        if (Int32.TryParse(EnterField.Text, out num) == false || num < 1 || num > ntw.Count) MessageBox.Show("Индекс вне диапазона!", "Эй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            update_num = num;
                            error = false;
                            new_quant = true;
                            MessageBox.Show("Для ввода нового количества нажмите на кнопку 'Обновить'!");
                        }
                    }
                }
                else
                {
                    if (new_price)
                    {
                        GetNewPrice();
                    }

                    if (new_quant)
                    {
                        GetNewQuantity();
                    }
                }
            }
        }

        private void ShowStore(object sender, RoutedEventArgs e) //Выводит на экран текущий магазин
        {
            PrintStore();
        }

        #region Requestes
        private void FindByPrice_clicked(object sender, RoutedEventArgs e) // Запрос с использованием Linq
        {
            if(ntw[current-1].Count==0) MessageBox.Show("Пустой магазин", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (!error) { MessageBox.Show("Для поиска введите стоимость в поле ввода!"); error = true; }
                else
                {
                    int price;
                    if(Int32.TryParse(EnterField.Text, out price) == false || price < 0) MessageBox.Show("Некорректная стоимость!", "Эй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        error = false;
                        var subset_price = from good in ntw[current - 1] where (good.Price == price) select good.ToString();
                        TestTextBox.Text = "Товары со стоимостью '" + price + "'\n";
                        foreach (var s in subset_price)
                            TestTextBox.Text += s.ToString() + "\n";
                    }
                }
            }
        }

        private void FindByQuantity_clicked(object sender, RoutedEventArgs e) // Запрос с использованием анонимных методов
        {
            if (ntw[current - 1].Count == 0) MessageBox.Show("Пустой магазин", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (!error) { MessageBox.Show("Для поиска введите количество в поле ввода!"); error = true; }
                else
                {
                    int quantity;
                    if (Int32.TryParse(EnterField.Text, out quantity) == false || quantity < 0) MessageBox.Show("Некорректное количество!", "Эй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        error = false;
                        Func<Goods, bool> isEqual = delegate (Goods good) { return quantity == good.Quantity; };
                        Func<Goods, string> toString = delegate (Goods good) { return good.ToString(); };
                        var subset_quantity = ntw[current - 1].Where(isEqual).Select(toString);
                        TestTextBox.Text = "Товары с количеством '" + quantity + "'\n";
                        foreach (var s in subset_quantity)
                            TestTextBox.Text += s.ToString() + "\n";
                    }
                }
            }

        }

        private void FindByName_clicked(object sender, RoutedEventArgs e) // Запрос с использованием расширяющих методов и лямбда выражений.
        {
            if (ntw[current - 1].Count == 0) MessageBox.Show("Пустой магазин", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (!error) { MessageBox.Show("Для поиска введите название товара в поле ввода!"); error = true; }
                else
                {
                    string name = EnterField.Text;
                    error = false;
                    var subset_name = ntw[current - 1].Where(g => (g.Name == name)).Select(g => g);
                    TestTextBox.Text = "Товары с названием '" + name + "'\n";
                    foreach (var s in subset_name)
                         TestTextBox.Text += s.ToString() + "\n";
                }
            }

        }
        #endregion

        #endregion

        #region Filters
        private void ShowIndustrial(object sender, RoutedEventArgs e)
        {
            TestTextBox.Text = "'" + ntw[current - 1].Name + "'";
            foreach (var s in ntw[current - 1])
                if(s.productionType == 2)TestTextBox.Text += "\n" + s.ToString();
        }

        private void ShowFood(object sender, RoutedEventArgs e)
        {
            TestTextBox.Text = "'" + ntw[current - 1].Name + "'";
            foreach (var s in ntw[current - 1])
                if (s.productionType == 1) TestTextBox.Text += "\n" + s.ToString();
        }


        #endregion

        #region Network_Menu
        public void Show_Network_clicked(object sender, RoutedEventArgs e)
        {
            PrintNetwork(ntw);
        }

        public void Choose_Store_clicked(object sender, RoutedEventArgs e)
        {
            if (!error) { MessageBox.Show("Введите номер магазина в поле ввода"); error = true; }
            else {
                if (ntw.Count == 0) MessageBox.Show("Выбор невозможен, пустая коллекция", "Хэй, очнись!",
    MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    int num;
                    if (Int32.TryParse(EnterField.Text, out num) == false || (num < 1) || (num > ntw.Count)) MessageBox.Show("Значение отсутствует или неверно введено. Введите в соответствующее поле верное значение", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        current = num;
                        MessageBox.Show("Выбран магазин с номером " + current.ToString(), "Сеть магазинов");
                        MessageBox.Show("Операции над элементами осуществляются только в этом магазине!", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        try
                        {
                            CurrentStore();
                        }
                        catch { MessageBox.Show("Error"); }
                        error = false;
                        Buttons_Work();
                    }
                }
            }
        }

        public void ClearNetwork_clicked(object sender, RoutedEventArgs e)
        {
            try {
                ntw.RemoveAll(p => (p.Name == p.Name));
            }
            catch
            {
                MessageBox.Show("Что-то не так!", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            MessageBox.Show("Общая коллекция была очищена!");
            BaseInfo(ntw.Name, ntw);
        }

        public void DeleteStore_clicked(object sender, RoutedEventArgs e)
        {
            if (ntw.Count == 0) MessageBox.Show("Пустая базовая коллекция!", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else {
                if (!error) { MessageBox.Show("Введите номер удаляемого магазина и нажмите кнопку 'Удалить'"); error = true; }
                else
                {
                    int num;
                    if (Int32.TryParse(EnterField.Text, out num) == false || num < 1 || num > ntw.Count) MessageBox.Show("Индекс вне границ коллекции", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        error = false;
                        try
                        {
                            ntw.RemoveAt_Event(num);
                        }
                        catch
                        {
                            MessageBox.Show("Проблема с RemoveAt_Event!", "Хэй, очнись!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        MessageBox.Show("Магазин был удален");
                        BaseInfo(ntw.Name, ntw);

                    }
                }
            }
        }

        public void Add_Store_clicked(object sender, RoutedEventArgs e)
        {
            ntw.Add_Store();
            MessageBox.Show("Магазин добавлен(пустой)");
            BaseInfo(ntw.Name, ntw);
        }

        #endregion

        #region menu
        private void Create_clicked(object sender, RoutedEventArgs e)
        {
            Buttons_SelectStore();
        }

        private void Save_clicked(object sender, RoutedEventArgs e) // Активация сохранения текущей коллекции в файл.
        {
            isSaved = true;
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("File.dat", FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, ntw);
                MessageBox.Show("Коллекция сохранена");
            }

        }

        private void Open_clicked(object sender, RoutedEventArgs e) // Загрузка коллекции из файла.
        { 
            if (!isSaved) MessageBox.Show("Открыть невозможно. Файл не существует", "Хэй, очнись!",
MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Buttons_SelectStore();
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream("File.dat", FileMode.OpenOrCreate))
                {
                    ntw = (NewNetwork)bf.Deserialize(fs);
                    MessageBox.Show("Коллекция загружена из файла");
                }
            }
        }

        private void Help_clicked(object sender, RoutedEventArgs e) //Открытие руководства пользователя.
        {
            System.Diagnostics.Process.Start(@"C:\Users\1\Documents\Visual Studio 2015\Projects\CourseWork.wpf\Manual.docx");
        }
        #endregion

        #region window_Current_Store_Info
        #region Network_info
        private void BaseInfo(string name, Network ntw)
        {
            CurrentStoreInfo.Text = "'" + name + "'";
            CurrentStoreInfo.Text += "\nПром. товары: " + GetCount_Ind(ntw).ToString();
            CurrentStoreInfo.Text += "\nПрод. товары: " + GetCount_Food(ntw).ToString();
            CurrentStoreInfo.Text += "\nВсего: " + SummaryCount(ntw).ToString();
            CurrentStoreInfo.Text += "\nМагазины: " + ntw.Count.ToString();
        }

        static private int GetCount_Ind(Network ntw)
        {
            int count = 0;
            foreach (var store in ntw)
                count += store.Count_Industial();
            return count;
        }

        static private int GetCount_Food(Network ntw)
        {
            int count = 0;
            foreach (var store in ntw)
                count += store.Count_Food();
            return count;
        }

        static private int SummaryCount(Network ntw)
        {
            int count = 0;
            foreach (var store in ntw)
                count += store.Count;
            return count;
        }
        #endregion

        #region current_store
        private void CurrentStore()
        {
            CurrentStoreInfo.Text = "'" + ntw[current - 1].Name + "'";
            CurrentStoreInfo.Text += "\nПром. товары: " + GetCount_Ind().ToString();
            CurrentStoreInfo.Text += "\nПрод. товары: " + GetCount_Food().ToString();
            CurrentStoreInfo.Text += "\nВсего: " + Summary().ToString();
        }

        private int GetCount_Ind()
        {
            return ntw[current - 1].Count_Industial();
        }

        private int GetCount_Food()
        {
            return ntw[current - 1].Count_Food();
        }

        private int Summary()
        {
            return ntw[current - 1].Count;
        }

        #endregion
        #endregion
    }
}

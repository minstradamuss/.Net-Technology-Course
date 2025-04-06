using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TsoiTourApp
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> _bigImg = new Dictionary<string, string>()
        {
            {"first1982", "Images/1982/1.jpg" },
            {"second1982", "Images/1982/2.jpg" },
            {"third1982", "Images/1982/3.jpg" },
            {"first1983", "Images/1983/1.jpg" },
            {"second1983", "Images/1983/2.jpg" },
            {"third1983", "Images/1983/3.jpg" }
        };

        private Dictionary<string, string> _bigImgText = new Dictionary<string, string>()
        {
            {"first1982", "Летом 1981 года в крымском поселке Морское компания молодых людей Алексей Рыбин, Виктор Цой и Олег Валинский создали новую группу, получившую название «Гарин и Гиперболоиды».  Сегодня мало кто помнит, что легендарная группа «КИНО» когда-то называлась именно так..." },
            {"second1982", "Цой в МИФИ - первый концерт. Москва, 20 октября 1982 года." },
            {"third1982", "Цой и Рыбин дома у Александра Липницкого. 25 июля 1982 года. " },
            {"first1983", "Борис Гребенщиков и Виктор Цой. Концерт в Металлострое. Ленинград, 1983." },
            {"second1983", "В феврале 1983 года Ленинградский рок-клуб запланировал очередную акцию — концерт групп побратимов по записи альбома «45» — «Аквариума» и «КИНО». Перед Цоем и Рыбиным, до этого выступавших «дуэтом на квартирниках», остро встает проблема расширения состава группы. А музыка Цоя, по словам Рыбина, уже тогда «могла звучать только в электричестве, с полным составом»." },
            {"third1983", "День Рождения Севы Гаккеля. «КИНО» в Рок-клубе. 19 февраля 1983 года." }
        };
        public Dictionary<string, string> BigImg
        {
            get { return _bigImg; }
            private set { }
        }
        public Dictionary<string, string> BigImgText
        {
            get { return _bigImgText; }
            private set { }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowBigImg(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)e.OriginalSource).Name;
            outputImage.Source = new BitmapImage(new Uri(BigImg[btnName], UriKind.Relative));
            outputText.Text = BigImgText[btnName];
        }

    }
}
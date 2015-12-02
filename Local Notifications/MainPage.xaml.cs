using Local_Notifications.Common;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml.Navigation;

using Windows.Data.Xml.Dom;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Local_Notifications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private XmlDocument tileXml;
        private XmlElement tileImage;
        private XmlNodeList tileList;
        private TileNotification notif;
        SecondaryTile imageTile, blockTile, badgeTile, iconicTile, cyclicTile;

        public MainPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForSquare150x150(true);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForWide310x150(true);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void ImageAndText_Toggled(object sender, RoutedEventArgs e)
        {
            if (ImageAndText.IsOn)
            {
                imageTile = new SecondaryTile(
                            "ImageAndText",
                            "Image and Text",
                            "Arguments",
                            new Uri("ms-appx:///Assets/blue.150x150.png", UriKind.Absolute),
                            TileSize.Square150x150);
                await imageTile.RequestCreateAsync();

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

                tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;
                tileImage.SetAttribute("src", "ms-appx:///Assets/blue.150x150.png");

                tileList = tileXml.GetElementsByTagName("text");
                (tileList[0] as XmlElement).InnerText = "Header text";
                (tileList[1] as XmlElement).InnerText = "First line of text";
                (tileList[2] as XmlElement).InnerText = "Second line of text";
                (tileList[3] as XmlElement).InnerText = "Third line of text";

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(imageTile.TileId).Update(notif);

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage02);

                tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;
                tileImage.SetAttribute("src", "ms-appx:///Assets/blue.310x150.png");

                tileList = tileXml.GetElementsByTagName("text");
                (tileList[0] as XmlElement).InnerText = "Header text";
                (tileList[1] as XmlElement).InnerText = "First line of text";
                (tileList[2] as XmlElement).InnerText = "Second line of text";
                (tileList[3] as XmlElement).InnerText = "Third line of text";
                (tileList[4] as XmlElement).InnerText = "Fourth line of text";

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
            }
            else
            {
                TileUpdateManager.CreateTileUpdaterForApplication().Clear();
                await imageTile.RequestDeleteAsync();
            }
        }

        private void ImageCollection_Toggled(object sender, RoutedEventArgs e)
        {
            if (ImageCollection.IsOn)
            {
                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageCollection);

                tileList = tileXml.GetElementsByTagName("image");
                (tileList[0] as XmlElement).SetAttribute("src", "ms-appx:///Assets/blue.310x150.png");
                (tileList[1] as XmlElement).SetAttribute("src", "ms-appx:///Assets/red.310x150.png");
                (tileList[2] as XmlElement).SetAttribute("src", "ms-appx:///Assets/green.310x150.png");
                (tileList[3] as XmlElement).SetAttribute("src", "ms-appx:///Assets/yellow.310x150.png");
                (tileList[4] as XmlElement).SetAttribute("src", "ms-appx:///Assets/orange.310x150.png");

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
            }
            else
            {
                TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            }
        }

        private async void CyclicToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (Cyclic.IsOn)
            {
                cyclicTile = new SecondaryTile(
                            "Cyclic",
                            "Cyclic",
                            "Arguments",
                            new Uri("ms-appx:///Assets/blue.150x150.png", UriKind.Absolute),
                            TileSize.Square150x150);
                await cyclicTile.RequestCreateAsync();
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(cyclicTile.TileId).EnableNotificationQueueForSquare150x150(true);

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
                tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;

                tileImage.SetAttribute("src", "ms-appx:///Assets/blue.150x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(cyclicTile.TileId).Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/red.150x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(cyclicTile.TileId).Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/green.150x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(cyclicTile.TileId).Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/yellow.150x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(cyclicTile.TileId).Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/orange.150x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(cyclicTile.TileId).Update(notif);

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Image);
                tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;

                tileImage.SetAttribute("src", "ms-appx:///Assets/blue.310x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/red.310x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/green.310x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/yellow.310x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
                tileImage.SetAttribute("src", "ms-appx:///Assets/orange.310x150.png");
                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
            }
            else
            {
                TileUpdateManager.CreateTileUpdaterForApplication().Clear();
                await cyclicTile.RequestDeleteAsync();
            }
        }

        private async void BadgeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (Badge.IsOn)
            {
                badgeTile = new SecondaryTile(
                            "BadgeTile",
                            "Badge",
                            "Arguments",
                            new Uri("ms-appx:///Assets/green.150x150.png", UriKind.Absolute),
                            TileSize.Square150x150);
                await badgeTile.RequestCreateAsync();

                tileXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);
                tileImage = tileXml.SelectSingleNode("/badge") as XmlElement;
                tileImage.SetAttribute("value", "alert");

                BadgeNotification badgeNotification = new BadgeNotification(tileXml);
                BadgeUpdateManager.CreateBadgeUpdaterForSecondaryTile(badgeTile.TileId).Update(badgeNotification);

                tileXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
                tileImage = tileXml.SelectSingleNode("/badge") as XmlElement;
                tileImage.SetAttribute("value", "31");
                badgeNotification = new BadgeNotification(tileXml);
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badgeNotification);
            }
            else
            {
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
                await badgeTile.RequestDeleteAsync();
            }
        }

        private async void IconicTile_Toggled(object sender, RoutedEventArgs e)
        {
            if (IconicTile.IsOn)
            {
                iconicTile = new SecondaryTile(
                            "IconicTile",
                            "Iconic",
                            "Arguments",
                            new Uri("ms-appx:///Assets/yellow.150x150.png", UriKind.Absolute),
                            TileSize.Square150x150);
                await iconicTile.RequestCreateAsync();

                tileXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
                tileImage = tileXml.SelectSingleNode("/badge") as XmlElement;
                tileImage.SetAttribute("value", "31");

                BadgeNotification badgeNotification = new BadgeNotification(tileXml);
                BadgeUpdateManager.CreateBadgeUpdaterForSecondaryTile(iconicTile.TileId).Update(badgeNotification);

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150IconWithBadge);

                tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;
                tileImage.SetAttribute("src", "ms-appx:///Assets/icon.130x202.png");

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(iconicTile.TileId).Update(notif);

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150IconWithBadgeAndText);

                tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;
                tileImage.SetAttribute("src", "ms-appx:///Assets/icon.70x110.png");

                tileList = tileXml.GetElementsByTagName("text");
                (tileList[0] as XmlElement).InnerText = "Header text";
                (tileList[1] as XmlElement).InnerText = "First line of text";
                (tileList[2] as XmlElement).InnerText = "Second line of text";

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
            }
            else
            {
                TileUpdateManager.CreateTileUpdaterForApplication().Clear();
                await iconicTile.RequestDeleteAsync();
            }
        }

        private async void BlockAndText_Toggled(object sender, RoutedEventArgs e)
        {
            if (BlockAndText.IsOn)
            {
                blockTile = new SecondaryTile(
                            "BlockAndText",
                            " ",
                            "Arguments",
                            new Uri("ms-appx:///Assets/orange.150x150.png", UriKind.Absolute),
                            TileSize.Square150x150);
                await blockTile.RequestCreateAsync();

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Block);

                tileList = tileXml.GetElementsByTagName("text");
                (tileList[0] as XmlElement).InnerText = "28";
                (tileList[1] as XmlElement).InnerText = "Nov";

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(blockTile.TileId).Update(notif);

                tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150BlockAndText01);

                tileList = tileXml.GetElementsByTagName("text");
                (tileList[0] as XmlElement).InnerText = "28";
                (tileList[1] as XmlElement).InnerText = "Nov";
                (tileList[2] as XmlElement).InnerText = "Indipendence Day of Albania";

                notif = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notif);
            }
            else
            {
                TileUpdateManager.CreateTileUpdaterForApplication().Clear();
                await blockTile.RequestDeleteAsync();
            }
        }
    }
}
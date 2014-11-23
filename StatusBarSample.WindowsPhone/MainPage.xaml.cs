using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace StatusBarSample
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StatusBarExtensions.LoadingWithProgress("进度条", async () =>
            {
                StatusBarExtensions.ShowMessage("完成");
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StatusBarExtensions.ShowMessage("你好");
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StatusBarExtensions.Loading("正在加载");
            await System.Threading.Tasks.Task.Delay(5000);
            StatusBarExtensions.StopLoading();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StatusBarExtensions.ShowMessage(" ", null, false);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            StatusBarExtensions.ShowMessage("StatusBarSample", null, false);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            StatusBarExtensions.ShowMessage("", null, false);
        }

    }
}

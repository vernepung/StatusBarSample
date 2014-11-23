#region 版 本 注 释
/****************************************************
	* 项 目 名: $projectname$
    * 文 件 名: StatusBarSample.WindowsPhone
    * Copyright © 2014 Verne Pung
    * 创 建 人: vernepung
    * 创建日期: 2014年11月23日 16:20:11
    ****************************************************** 
    * 修 改 人：
    * 修改日期：
    * 备注描述：
    *  
    * 
*******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;

namespace StatusBarSample
{
    public static class StatusBarExtensions
    {
        private static bool _isLoading = false;
        public static StatusBar StatusBar
        {
            get
            {
                return StatusBar.GetForCurrentView();
            }
        }
        private static CoreApplicationView coreApplication
        {
            get
            {
                return CoreApplication.GetCurrentView();
            }
        }
        public static void LoadingWithProgress(string text, Action completedAction)
        {
            coreApplication.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async delegate
            {
                Random random = new Random();
                _isLoading = true;
                StatusBar.ProgressIndicator.ProgressValue = 0;

                StatusBar.ProgressIndicator.Text = text;
                await StatusBar.ProgressIndicator.ShowAsync();
                while (_isLoading)
                {
                    await Task.Delay(random.Next(200, 500));
                    if (_isLoading)
                    {
                        if (StatusBar.ProgressIndicator.ProgressValue >= 1)
                        {
                            _isLoading = false;
                            break;
                        }
                        StatusBar.ProgressIndicator.ProgressValue += random.NextDouble() / 10.0;
                    }
                    else
                        break;
                }
                if (completedAction != null)
                    completedAction.Invoke();
            });
        }
        public static void Loading(string text)
        {
            coreApplication.Dispatcher.RunAsync(
                 Windows.UI.Core.CoreDispatcherPriority.Normal,
                 async delegate
                 {
                     _isLoading = true;
                     StatusBar.ProgressIndicator.ProgressValue = null;
                     StatusBar.ProgressIndicator.Text = text;
                     await StatusBar.ProgressIndicator.ShowAsync();
                 });
        }
        public static void StopLoading()
        {
            coreApplication.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                async delegate
                {
                    if (_isLoading)
                    {
                        await StatusBar.ProgressIndicator.HideAsync();
                        _isLoading = false;
                        StatusBar.ProgressIndicator.ProgressValue = 0;
                    }
                });
        }

        public static void ShowMessage(string text, TimeSpan? timeSpan = null, bool allowHide = true)
        {
            CoreApplication.GetCurrentView().Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async delegate
            {
                StopLoading();

                StatusBar.ProgressIndicator.Text = text;
                StatusBar.ProgressIndicator.ProgressValue = 0;
                await StatusBar.ProgressIndicator.ShowAsync();
                if (!allowHide) return;
                if (timeSpan == null)
                    timeSpan = TimeSpan.FromSeconds(3);
                await Task.Delay((TimeSpan)timeSpan);
                await CoreApplication.GetCurrentView().Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async delegate
                {
                    await StatusBar.ProgressIndicator.HideAsync();
                });
            });
        }
    }
}

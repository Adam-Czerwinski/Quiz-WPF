﻿using System;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Views;


namespace Quiz.Source.Dialogs
{
    public class DialogService : IDialogService
    {

        /// <summary>
        /// Displays information about an error.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => MessageBox.Show(message, title, MessageBoxButton.OK));
            afterHideCallback?.Invoke();
        }

        /// <summary>
        /// Displays information about an error.
        /// </summary>
        /// <param name="error">The exception of which the message must be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => MessageBox.Show(error.Message, title, MessageBoxButton.OK));
            afterHideCallback?.Invoke();
        }

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button with the text "OK".
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        public async Task ShowMessage(string message, string title)
        {
            await Task.Run(() => MessageBox.Show(message, title));
        }

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => MessageBox.Show(message, title, MessageBoxButton.OK));
            afterHideCallback?.Invoke();
        }

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonConfirmText">The text shown in the "confirm" button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="buttonCancelText">The text shown in the "cancel" button
        /// in the dialog box. If left null, the text "Cancel" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button
        /// (false) was pressed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        public async Task<bool> ShowMessage(string message,string title,string buttonConfirmText,string buttonCancelText,Action<bool> afterHideCallback)
        {

            MessageBoxResult messageBoxResult = await Task.Run(() => MessageBox.Show(message, title, MessageBoxButton.YesNo));

            bool result;
            if (messageBoxResult == MessageBoxResult.Yes)
                result = true;
            else
                result = false;

            afterHideCallback?.Invoke(result);

            return result;
        }

        /// <summary>
        /// Displays information to the user in a simple dialog box. The dialog box will have only
        /// one button with the text "OK". This method should be used for debugging purposes.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        public async Task ShowMessageBox(string message, string title)
        {
            await Task.Run(() => MessageBox.Show(message, title));
        }

        public DialogService() { }
    }
}

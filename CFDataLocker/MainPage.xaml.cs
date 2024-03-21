﻿using CFDataLocker.Models;

namespace CFDataLocker
{
    /// <summary>
    /// Main page. Lists data items
    /// </summary>
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly MainPageModel _model;        
     
        public MainPage(MainPageModel mainPageModel)
        {
            InitializeComponent();         

            //var result = LocalizationResourceManager.Instance["MainAddButtonText"];
            //var result = LocalizationResourceManager[""];

            _model = mainPageModel;
            this.BindingContext = _model;

            DataItemsList.ItemSelected += DataItemsList_ItemSelected;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // Refresh items if coming from edit page
            _model.RefreshDataItems(_model.SelectedDataItem?.Id);                                            
        }

        private void EditDataItem(DataItemBase dataItem)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "LockerId", _model.DataLockerId },
                    { "ItemId", dataItem.Id }
                };

            //Shell.Current.GoToAsync($"{nameof(EditDataItemPage)}?ItemId={dataItem.Id}");

            // TODO: Clean this up, move to another class
            if (dataItem is DataItemDefault)
            {
                Shell.Current.GoToAsync(nameof(EditDataItemPage), parameters);
            }
            else if (dataItem is DataItemBankAccount)
            {
                Shell.Current.GoToAsync(nameof(EditBankAccountPage), parameters);
            }
            else if (dataItem is DataItemCreditCard)
            {
                Shell.Current.GoToAsync(nameof(EditCreditCardPage), parameters);
            }
            else if (dataItem is DataItemDocument)
            {
                Shell.Current.GoToAsync(nameof(EditDocumentPage), parameters);
            }
        }

        private void DataItemsList_ItemSelected(object? sender, SelectedItemChangedEventArgs e)
        {
            /* Don't edit on select, need to be able to delete selected data item
            if (e.SelectedItem != null)
            {
                var dataItem = (DataItem)e.SelectedItem;
                EditDataItem(dataItem);
            }
            */
        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            Type dataItemType = _model.SelectedDataItemType.Name switch
            {                
                "Bank Account" => typeof(DataItemBankAccount),
                "Credit Card" => typeof(DataItemCreditCard),
                "Document" => typeof(DataItemDocument),
                _ => typeof(DataItemDefault)                
            };

            var dataItem = _model.AddDataItem(_model.LocalizationResources["New"].ToString(), dataItemType);
            EditDataItem(dataItem);
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            var dataItem = (DataItemBase)DataItemsList.SelectedItem;
            //var isDelete = await DisplayAlert("Delete", $"Do you want to delete {dataItem.Name}?", "Yes", "No");
            var isDelete = await DisplayAlert(_model.LocalizationResources["Delete"].ToString(),
                                String.Format(_model.LocalizationResources["DeleteDataItemPrompt"].ToString(), dataItem.Name),
                                _model.LocalizationResources["Yes"].ToString(),
                                _model.LocalizationResources["No"].ToString());

            if (isDelete)
            {
                _model.DeleteDataItem(dataItem.Id);
            }
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            var dataItem = (DataItemBase)DataItemsList.SelectedItem;
            if (dataItem != null)
            {
                EditDataItem(dataItem);
            }
        }

        private void DataItemTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Type dataItemType = _model.SelectedDataItemType.Name switch
            //{
            //    "Bank Account" => typeof(DataItemBankAccount),
            //    "Credit Card" => typeof(DataItemCreditCard),
            //    _ => typeof(DataItemDefault)
            //};

            //var dataItem = _model.AddDataItem(_model.LocalizationResources["New"].ToString(), dataItemType);
            //EditDataItem(dataItem);
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }

}
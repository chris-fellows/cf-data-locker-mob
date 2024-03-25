using CFDataLocker.ViewModels;

namespace CFDataLocker;

/// <summary>
/// Edit data item page
/// </summary>
[QueryProperty(nameof(DataLockerId), "LockerId")]
[QueryProperty(nameof(DataItemId), "ItemId")]
public partial class EditDocumentPage : ContentPage
{
    private readonly EditDocumentPageModel _model;

	public EditDocumentPage(EditDocumentPageModel model)
	{
		InitializeComponent();

        _model = model;
        this.BindingContext = _model;
    }

    public string DataLockerId
    {
        set
        {
            _model.DataLockerId = value;
        }
    }

    public string DataItemId
    {
        set
        {
            _model.DataItemId = value;
        }
    }

    private void CancelBtn_Clicked(object sender, EventArgs e)
    {
        // Navigate to parent
        Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Returns validation errors
    /// </summary>
    /// <returns></returns>
    private List<string> GetValidationErrors()
    {
        var messages = new List<string>();

        if (NameValidator.IsNotValid) messages.Add(_model.LocalizationResources["EditNameInvalid"].ToString());

        //if (!String.IsNullOrEmpty(ContactEmailEntry.Text) && ContactEmailValidator.IsNotValid)
        //{
        //    messages.AddRange(ContactEmailValidator.Errors.Select(error => error.ToString()));
        //}

        //if (!String.IsNullOrEmpty(ContactPhoneEntry.Text) && ContactPhoneValidator.IsNotValid)
        //{
        //    messages.Add(_model.LocalizationResources["EditContactPhoneInvalid"].ToString());
        //}

        //if (!String.IsNullOrEmpty(URLEntry.Text) && URLValidation.IsNotValid)
        //{
        //    messages.Add(_model.LocalizationResources["EditURLInvalid"].ToString());
        //}

        return messages;
    }

    private void SaveBtn_Clicked(object sender, EventArgs e)
    {
        var messages = GetValidationErrors();
        if (messages.Any()) // Invalid
        {
            DisplayAlert(_model.LocalizationResources["Error"].ToString(), messages[0], 
                        _model.LocalizationResources["OK"].ToString());
        }
        else   // Save
        {
            _model.SaveChanges();

            // Navigate to parent
            Shell.Current.GoToAsync("..");
        }
    }

    private async void SelectDocumentBtn_Clicked(object sender, EventArgs e)
    {
        var pickOptions = new PickOptions() { PickerTitle = "Select document" };

        try
        {
            // Select file
            var result = await FilePicker.Default.PickAsync(pickOptions);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    // Update file
                    _model.SelectDocumentFilePath(result.FullPath);

                    //using var stream = await result.OpenReadAsync();
                    //var image = ImageSource.FromStream(() => stream);
                }
            }

            // Inform user that they can delete the original document for security reasons if necessary
            await DisplayAlert(_model.LocalizationResources["Information"].ToString(),
                    _model.LocalizationResources["DocumentFileSavedText"].ToString(),
                    _model.LocalizationResources["OK"].ToString());

            int xxx = 1000;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
            int xxxxxx = 1000;
        }
    }
}
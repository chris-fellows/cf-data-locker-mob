using CFDataLocker.ViewModels;

namespace CFDataLocker;


[QueryProperty(nameof(DataLockerId), "LockerId")]
[QueryProperty(nameof(DataItemId), "ItemId")]
public partial class EditCreditCardPage : ContentPage
{
    private readonly EditCreditCardPageModel _model;

	public EditCreditCardPage(EditCreditCardPageModel model)
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

        if (!String.IsNullOrEmpty(CardNumberEntry.Text) && CardNumberValidator.IsNotValid)
        {
            messages.Add(_model.LocalizationResources["EditContactPhoneInvalid"].ToString());
        }

        if (!String.IsNullOrEmpty(SecurityCodeEntry.Text) && SecurityCodeValidator.IsNotValid)
        {
            messages.Add(_model.LocalizationResources["EditURLInvalid"].ToString());
        }


        if (!String.IsNullOrEmpty(ExpiryDateEntry.Text) && ExpiryDateValidator.IsNotValid)
        {
            messages.Add(_model.LocalizationResources["EditURLInvalid"].ToString());
        }

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
}
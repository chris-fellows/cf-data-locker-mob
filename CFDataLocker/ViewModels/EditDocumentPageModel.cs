using CFDataLocker.Exceptions;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CFDataLocker.ViewModels
{
    /// <summary>
    /// Model for edit of document data item
    /// </summary>
    public class EditDocumentPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public LocalizationResources LocalizationResources => LocalizationResources.Instance;

        private DataLocker _dataLocker = new DataLocker();
        private DataItemDocument _dataItem = new DataItemDocument();
        private readonly IDataLockerService _dataLockerService;
        private readonly IEncryptionService _encryptionService;
        private readonly ISecureItemService _secureItemService;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public EditDocumentPageModel(IDataLockerService dataLockerService,
            IEncryptionService encryptionService,
                ISecureItemService secureItemService)
        {
            _dataLockerService = dataLockerService;
            _encryptionService = encryptionService;
            _secureItemService = secureItemService;
        }

        private string _dataLockerId;
        public string DataLockerId
        {
            set
            {
                _dataLockerId = value;
                OnQueryPropertySet();
            }
        }

        private string _dataItemId;

        public string DataItemId
        {
            set
            {
                _dataItemId = value;
                OnQueryPropertySet();
            }
        }

        /// <summary>
        /// Handles incoming query property set
        /// </summary>
        private void OnQueryPropertySet()
        {
            if (!string.IsNullOrEmpty(_dataLockerId) &&
                !string.IsNullOrEmpty(_dataItemId))     // Load data locker & item
            {
                _dataLocker = _dataLockerService.GetById(_dataLockerId);
                _dataItem = (DataItemDocument)_dataLocker.DataItems.First(di => di.Id == _dataItemId);

                OnPropertyChanged(nameof(DocumentImageSource));
                OnPropertyChanged(nameof(SelectedDataItem));
            }
        }

        public DataItemDocument SelectedDataItem
        {
            get { return _dataItem; }
        }

        public void SaveChanges()
        {
            _dataLockerService.Update(_dataLocker);
        }

        /// <summary>
        /// Document as ImageSource
        /// </summary>
        public ImageSource DocumentImageSource
        {
            get
            {
                if (_dataItem != null && !string.IsNullOrEmpty(_dataItem.FilePath))
                {
                    try
                    {
                        var fileBytes = _encryptionService.DecryptFromByteArray(File.ReadAllBytes(_dataItem.FilePath));
                        MemoryStream memoryStream = new MemoryStream(fileBytes);
                        var imageSource = ImageSource.FromStream(() => memoryStream);
                        return imageSource;
                    }
                    catch (Exception exception)
                    {
                        throw new DataLockerException($"Document file is corrupt", exception);
                    }
                }
                else if (!File.Exists(_dataItem.FilePath))
                {
                    throw new FileNotFoundException("Document file does not exist");
                }
                return null;
            }
        }

        /// <summary>
        /// Selects the document file from storage and creates an encrypted version in the app's folder        
        /// </summary>
        /// <param name="sourceFilePath"></param>
        public void SelectDocumentFilePath(string sourceFilePath)
        {
            // Create local encrypted file
            var localFile = Path.Combine(FileSystem.AppDataDirectory, "DataItemDocuments", $"{Guid.NewGuid()}.bin");
            Directory.CreateDirectory(Path.GetDirectoryName(localFile));
            File.WriteAllBytes(localFile, _encryptionService.EncryptToByteArray(File.ReadAllBytes(sourceFilePath)));

            // Set local file
            _dataItem.FilePath = localFile;

            OnPropertyChanged(nameof(DocumentImageSource));
        }
    }
}

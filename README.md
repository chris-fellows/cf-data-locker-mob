# cf-data-locker-mob

Mobile app (.NET MAUI) for securely storing a list of data items. Encryption uses AES.

Currently only Android is targeted but iOS will be targeted later.

Data Item Types
---------------
- General. E.g. Website credentials, utilities account number, NHS number, NI number.
- Bank account details.
- Credit card details.
- Document. E.g. Driving licence photo, birth certificate photo. When the document is selected
then an encrypted copy is created and so the original document can be deleted from the device
for security reasons.

How to Add a Document (E.g. Photo)
----------------------------------
1) Take a photo or upload the photo to the device. E.g. Driving licence.
2) From the app then add a data item of type "Document" and select the photo.
3) Delete the original photo for security purposes.

Known Issues
------------
- UI needs cleaning up.
- Models for data item views need to be refactored to share code because they're similar.
- Document image needs functions to show the image full screen.
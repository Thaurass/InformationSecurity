using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class VigenereEncryptionInterface : ContentPage
{
    VigenereEncryptionLogic _vigenereEncryptionLogic = new();

    public VigenereEncryptionInterface()
	{
		InitializeComponent();
	}

    public void Encrypt_Click(object sender, EventArgs args)
    {
        EncryptText.Text = string.Empty;
        if (Check_empty(Key) && Check_empty(UnencryptText))
        {
            EncryptText.Text = _vigenereEncryptionLogic.Encrypt_func_Vigener(UnencryptText.Text, Key.Text);
        }
    }
    public void Decrypt_Click(object sender, EventArgs args)
    {
        Data.Text = string.Empty;

        if (Check_empty(Key) && Check_empty(UnencryptText))
        {
            Data.Text = _vigenereEncryptionLogic.Unencrypt_func_Vigener(UnencryptText.Text, Key.Text);
        }
    }

    private async void SaveButton_Click(object sender, EventArgs args)
    {
        await _vigenereEncryptionLogic.SaveCipherData(UnencryptText.Text + "|" + Key.Text);
        if (_vigenereEncryptionLogic.IsSaveSuccessful)
        {
            await Toast.Make($"File is saved").Show();
        }
        else
        {
            await Toast.Make($"File is not saved").Show();
        }
    }

    public async void LoadFile(object sender, EventArgs args)
    {
        await _vigenereEncryptionLogic.GetCipherData();
        UnencryptText.Text = _vigenereEncryptionLogic.FileData[0];
        Key.Text = _vigenereEncryptionLogic.FileData[1];
    }

    public bool Check_empty(Entry sender)
    {
        bool err = true;
        if (sender.Text == string.Empty) { err = false; }
        return err;

    }

}
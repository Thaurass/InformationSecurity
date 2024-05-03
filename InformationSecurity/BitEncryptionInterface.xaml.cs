using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class BitEncryptionInterface : ContentPage
{
	public BitEncryptionInterface()
	{
		InitializeComponent();
    }

    BitEncryptionLogic _bitEncryptionLogic = new();

    public void Encrypt_Click(object sender, EventArgs args)
    {
        Data.Text = string.Empty;

        OutputText.Text = string.Empty;
        if (Check_empty(InputText))
        {
            OutputText.Text = _bitEncryptionLogic.Encrypt_func(InputText.Text);

            for (int i = 0; i < _bitEncryptionLogic.TempData.Length; i++)
            {
                Data.Text += _bitEncryptionLogic.TempData[i] + "\n";
            }
        }
    }

    public void Decrypt_Click(object sender, EventArgs args)
    {
        Data.Text = string.Empty;

        if (Check_empty(InputText))
        {
            OutputText.Text = _bitEncryptionLogic.Unencrypt_func(InputText.Text);

            for (int i = 0; i < _bitEncryptionLogic.TempData.Length; i++)
            {
                Data.Text += _bitEncryptionLogic.TempData[i] + "\n";
            }
        }
    }

    private async void SaveButton_Click(object sender, EventArgs args)
    {
        await _bitEncryptionLogic.SaveCipherData(InputText.Text + "|" + "1");
        if (_bitEncryptionLogic.IsSaveSuccessful)
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
        await _bitEncryptionLogic.GetCipherData();
        if (_bitEncryptionLogic.FileData[0] != "first_line")
        {
            InputText.Text = _bitEncryptionLogic.FileData[0];
        }

    }

    public bool Check_empty(Entry sender)
    {
        bool err = true;
        if (sender.Text == string.Empty) { err = false; }
        return err;

    }

    public bool Check_number(Entry sender)
    {
        bool err = false;
        int step;
        if (int.TryParse(sender.Text, out step))
        {
            if (step > 0 && step <= 50) { err = true; }
        }
        return err;

    }

    private void ClearEntry_Click(object sender, EventArgs args)
    {
        OutputText.Text = string.Empty;
        InputText.Text = string.Empty;
        Data.Text = string.Empty;
    }
}
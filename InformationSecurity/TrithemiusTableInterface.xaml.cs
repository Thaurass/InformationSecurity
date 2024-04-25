using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class TrithemiusTableInterface : ContentPage
{
	public TrithemiusTableInterface()
	{
		InitializeComponent();
	}

    TrithemiusTableLogic _trithemiusTableLogic = new();

    public void Encrypt_Click(object sender, EventArgs args)
    {
        if (Check_empty(InputText))
        {
            ResultText.Text = _trithemiusTableLogic.Encrypt_func_Trithemius(InputText.Text);
            input_entropy.Text = "Ёнтропи€ исходного текста: " + _trithemiusTableLogic.Message_entropy(InputText.Text);
            result_entropy.Text = "Ёнтропи€ результата: " + _trithemiusTableLogic.Message_entropy(ResultText.Text);
        }
        
    }
    public void Decrypt_Click(object sender, EventArgs args)
    {
        if (Check_empty(InputText))
        {
            ResultText.Text = _trithemiusTableLogic.Unencrypt_func_Trithemius(InputText.Text);
            input_entropy.Text = "Ёнтропи€ исходного текста: " + _trithemiusTableLogic.Message_entropy(InputText.Text);
            result_entropy.Text = "Ёнтропи€ результата: " + _trithemiusTableLogic.Message_entropy(ResultText.Text);
        }
    }

    private async void SaveButton_Click(object sender, EventArgs args)
    {
        await _trithemiusTableLogic.SaveCipherData(InputText.Text + "|" + "1");
        if (_trithemiusTableLogic.IsSaveSuccessful)
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
        await _trithemiusTableLogic.GetCipherData();
        if (_trithemiusTableLogic.FileData[0] != "first_line")
        {
            InputText.Text = _trithemiusTableLogic.FileData[0];
        }
        
    }

    public bool Check_empty(Entry sender)
    {
        bool err = true;
        if (sender.Text == string.Empty) { err = false; }
        return err;

    }
}
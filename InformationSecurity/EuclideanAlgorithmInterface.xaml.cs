using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class EuclideanAlgorithmInterface : ContentPage
{
	public EuclideanAlgorithmInterface()
	{
		InitializeComponent();
        _dictonaries.RusProbilities();
        _dictonaries.EngProbilities();
    }

    CaesarCipherLogic _caesarCipherLogic = new();
    Dictonaries _dictonaries = new();




    public void Encrypt_Click(object sender, EventArgs args)
    {
        EncryptText.Text = string.Empty;
        if (Check_number(Step) && Check_empty(UnencryptText))
        {
            EncryptText.Text = _caesarCipherLogic.Encrypt_func(UnencryptText.Text, Step.Text);
        }
    }
    public void Decrypt_Click(object sender, EventArgs args)
    {
        Data.Text = string.Empty;

        if (Check_empty(UnencryptText))
        {
            Data.Text = _caesarCipherLogic.DecryptMessage(UnencryptText.Text);
        }
    }


    private async void SaveButton_Click(object sender, EventArgs args)
    {
        await _caesarCipherLogic.SaveCipherData(UnencryptText.Text + "|" + Step.Text);
        if (_caesarCipherLogic.IsSaveSuccessful)
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
        await _caesarCipherLogic.GetCipherData();
        if (_caesarCipherLogic.FileData[0] != "first_line")
        {
            UnencryptText.Text = _caesarCipherLogic.FileData[0];
            Step.Text = _caesarCipherLogic.FileData[1];
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
        EncryptText.Text = string.Empty;
        UnencryptText.Text = string.Empty;
        Step.Text = string.Empty;
        Data.Text = string.Empty;
    }
}
using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class PspGeneratorInterface : ContentPage
{
	public PspGeneratorInterface()
	{
		InitializeComponent();
	}

    BitEncryptionLogic _bitEncryptionLogic = new();
    PspGeneratorLogic _pspGeneratorLogic = new();

    public void Encrypt_Click(object sender, EventArgs args)
    {
        if (Check_number(Step.Text, 1, 10000) && Check_empty(Step))
        {
            if (InputText.Text == "0" || (Check_Str(InputText.Text, "01") && InputText.Text.Length == 32))
            {
                Data.Text = _pspGeneratorLogic.Encrypt_func_PSP(InputText.Text, Step.Text);
            }
            else
            {
                Data.Text = "NOT OK";
            }
        }
    }

    private async void SaveButton_Click(object sender, EventArgs args)
    {
        await _bitEncryptionLogic.SaveCipherData(InputText.Text + "|" + Step.Text);
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
            Step.Text = _bitEncryptionLogic.FileData[1];
        }

    }

    private bool Check_Str(string str, string Leters)
    {
        bool err = true;
        if (str != null)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (Check_Char(str[i], Leters)) { err = false; }
            }
        }
        


        return err;

    }

    private bool Check_Char(char c, string Leters)
    {
        bool err = true;
        string s = Leters;

        if (s.IndexOf(c) != -1) { err = false; }

        return err;

    }

    public bool Check_empty(Entry sender)
    {
        bool err = true;
        if (sender.Text == string.Empty) { err = false; }
        return err;

    }

    private bool Check_number(string sender, int lover, int up)
    {
        bool err = false;
        int step;
        if (int.TryParse(sender, out step))
        {
            if (step >= lover && step <= up) { err = true; }
        }
        return err;

    }

    private void ClearEntry_Click(object sender, EventArgs args)
    {
        Step.Text = string.Empty;
        InputText.Text = string.Empty;
        Data.Text = string.Empty;
    }
}
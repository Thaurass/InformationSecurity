using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class PrimeNumbersInterface : ContentPage
{
	public PrimeNumbersInterface()
	{
		InitializeComponent();
	}

    BitEncryptionLogic _bitEncryptionLogic = new();
    PrimeNumbersLogic _primeNumbersLogic = new();


    public void Encrypt_Click(object sender, EventArgs args)
    {


        if (Check_number(Step.Text, 1, 100000) && Check_empty(Step))
        {
            if (InputText.Text == null)
            {
                Data.Text = _primeNumbersLogic.FunctionAutomatisation(Step.Text);
            }
            else
            {
                if (ulong.TryParse(InputText.Text, out ulong InputNum) && int.TryParse(Step.Text, out int StepNum))
                {

                    Data.Text = _primeNumbersLogic.RabinSimplicityTest(InputNum, StepNum).ToString();

                }
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
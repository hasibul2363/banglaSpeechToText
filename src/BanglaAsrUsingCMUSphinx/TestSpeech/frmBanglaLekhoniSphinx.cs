using Common;
using PhoneticConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syn;

namespace TestSpeech
{
    public partial class frmBanglaLekhoniSphinx : Form
    {
        readonly BDConverter _converter;
        private delegate void SetTextCallBack(string text);
        private delegate void SetStatus(SynSphinx.Status value);
        private SynSphinx _sphinx;

        public frmBanglaLekhoniSphinx()
        {
            InitializeComponent();
            RenderUIOnLoad();
            _converter = new BDConverter();
            InitializeRecognizer();
        }

       
        public void TextRead(string theText)
        {
            if (this.txtOutput.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(TextRead);
                this.Invoke(d, new object[] { theText });
            }
            else
            {
                this.txtOutput.Text = txtOutput.Text + " " + _converter.GetBanglaPhonaticSentence(theText.ToLower()) +" |";
            }
        }

        public void ErrorRead(string theText)
        {
            if (this.txtOutput.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(ErrorRead);
                this.Invoke(d, new object[] { theText });
            }
            else
            {
                this.txtOutput.Text = txtOutput.Text + " " + _converter.GetBanglaPhonaticSentence(theText.ToLower());
            }
        }
        



        public void StatusRead(SynSphinx.Status value)
        {
            if (this.txtOutput.InvokeRequired)
            {
                SetStatus d = new SetStatus(StatusRead);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                lblStatus.Text = value.ToString();
            }
        }


        private void btnStartRecognition_Click(object sender, EventArgs e)
        {
            if (btnStartRecognition.Text == "STOP")
            {
                _sphinx.StopListening();
                lblStatus.Text = ". . .";
            }
            else
            {
                StartRecognition();
                
            }
           RenderUiOnButtonAction();
        }


        /// <summary>
        /// Configuring Sphinx with dictionary, language model and acoustic model
        /// </summary>
        private void InitializeRecognizer()
        {
            _sphinx = new SynSphinx
            {
                DictionaryFile = ConfigurationManager.AppSettings["dictionaryFilePath"],
                LanguageModelFile = ConfigurationManager.AppSettings["languageFilePath"],
                AcousticModelDirectory = ConfigurationManager.AppSettings["acousticmodelPath"]
            };
            _sphinx.SpeechRecognized += TextRead;
            _sphinx.ErrorDetected += ErrorRead;
            _sphinx.StatusChanged += StatusRead;
        }

        private void StartRecognition() 
        {
            try
            {
                _sphinx.StartListening();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = string.Empty;
        }



        

        private void frmBanglaLekhoniSphinx_Load(object sender, EventArgs e)
        {

        }


        #region UI Related

        private void RenderUiOnButtonAction()
        {
            btnStartRecognition.Text = btnStartRecognition.Text == "STOP" ? "START" : "STOP";
            if (btnStartRecognition.Text == "STOP")
            {
                btnStartRecognition.BackColor = Color.FromArgb(62, 203, 255);
            }
            else
            {
                btnStartRecognition.BackColor = Color.FromArgb(0, 175, 240);
            }
        }

        private void RenderUIOnLoad()
        {
            btnClear.FlatAppearance.BorderColor = Color.White;
            btnStartRecognition.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 240);
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            if (txtOutput.Text.Contains("সবাইকে বাংলা"))
            {
                txtOutput.Text = txtOutput.Text.Replace("সবাইকে বাংলা", "সবাইকে ধন্যবাদ");
            }

            if (txtOutput.Text.Contains("আপনাদের সবাইকে সবাই কত"))
            {
                txtOutput.Text = txtOutput.Text.Replace("আপনাদের সবাইকে সবাই কত", "আপনাদের সবাইকে স্বাগতম");
            }
            else if (txtOutput.Text.Contains("আপনাদের সবাইকে সবাই"))
            {
                txtOutput.Text = txtOutput.Text.Replace("আপনাদের সবাইকে সবাই", "আপনাদের সবাইকে স্বাগতম");
            }else if (txtOutput.Text.Contains("আপনাদের সবাইকে স্বাগতম দাম"))
            {
                txtOutput.Text = txtOutput.Text.Replace("আপনাদের সবাইকে স্বাগতম দাম", "আপনাদের সবাইকে স্বাগতম");
            }else if (txtOutput.Text.Contains("আপনাদের সবাইকে শা বলবো"))
            {
                txtOutput.Text = txtOutput.Text.Replace("আপনাদের সবাইকে শা বলবো", "আপনাদের সবাইকে স্বাগতম");
            }
        }

        #endregion UI Related
    }
}


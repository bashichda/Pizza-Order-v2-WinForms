using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstWindowsForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        float GetSelectedSizePrice()
        {
            if (rbSmall.Checked)
                return Convert.ToSingle(rbSmall.Tag);
            else if (rbMedium.Checked)
                return Convert.ToSingle(rbMedium.Tag);
            else
                return Convert.ToSingle(rbLarge.Tag);
        }

        float CalculateToppingsPrice()
        {
            float ToppingsPrice = 0;

            if (chkExtraaCheese.Checked)
                ToppingsPrice += Convert.ToSingle(chkExtraaCheese.Tag);
            if (chkMushrooms.Checked)
                ToppingsPrice += Convert.ToSingle(chkMushrooms.Tag);
            if (chkTomatoes.Checked)
                ToppingsPrice += Convert.ToSingle(chkTomatoes.Tag);
            if (chkOnion.Checked)
                ToppingsPrice += Convert.ToSingle(chkOnion.Tag);
            if (chkOlives.Checked)
                ToppingsPrice += Convert.ToSingle(chkOlives.Tag);
            if (chkGreenPappers.Checked)
                ToppingsPrice += Convert.ToSingle(chkGreenPappers.Tag);

            return ToppingsPrice;
        }

        float GetSelectedCrustPrice()
        {
            if (rbThick.Checked)
                return Convert.ToSingle(rbThick.Tag);
            else
                return Convert.ToSingle(rbThin.Tag);
        }
        float CalculateTotalPrice()
        {
            return GetSelectedSizePrice() + CalculateToppingsPrice() + GetSelectedCrustPrice(); 
        }
        void UpdateTotalPrice()
        {
            lblTotalPrice.Text = CalculateTotalPrice().ToString() + "$"; 
        }
        void UpdateSize()
        {
            UpdateTotalPrice();

            if (rbSmall.Checked)
            {
                lblSize.Text = "Small";
                return;
            }
            else if (rbMedium.Checked)
            {
                lblSize.Text = "Meduim";
                return;
            }
            else if (rbLarge.Checked)
            {
                lblSize.Text = "Large";
                return;
            }
        }

        void UpdateCrust()
        {
            UpdateTotalPrice();

            if (rbThin.Checked)
            {
                lblCrustType.Text = "Thin Crust";
                return;
            }

            if (rbThick.Checked)
            {
                lblCrustType.Text = "Thinck Crust";
                return;
            }
        }   

        void UpdateToppings()
        {
            UpdateTotalPrice();
            string stTopppings = "";

            if (chkExtraaCheese.Checked)
            {
                stTopppings += "Extraa Cheese";
            }

            if (chkMushrooms.Checked)
            {
                stTopppings += ", Mushrooms";
            }

            if (chkTomatoes.Checked)
            {
                stTopppings += ", Tomatoes";
            }

            if (chkOnion.Checked)
            {
                stTopppings += ", Onion";
            }

            if (chkOlives.Checked)
            {
                stTopppings += ", Olives";
            }

            if (chkGreenPappers.Checked)
            {
                stTopppings += ", Greeen Pappers";
            }

            if (stTopppings == "")
            {
                stTopppings = "No Toppings";
            }
                lblToppings.Text = stTopppings;
        }

        void UpdateWhereToEat()
        {
            UpdateTotalPrice();

            if (rbEatIn.Checked)
            {
                lblWhereToEat.Text = "Eat In";
                return;
            }

            if (rbTakeOut.Checked)
            {
                lblWhereToEat.Text = "Take Out";
                return;
            }
        }

        void ResetForm()
        {
            gbSize.Enabled = true;
            gbToppings.Enabled = true;
            gbCrustType.Enabled = true;
            gbWhereToEat.Enabled = true;

            //Reset size : 
            rbMedium.Checked = true;

            //Reset Toppings :
            chkExtraaCheese.Checked = false;
            chkMushrooms.Checked = false;
            chkTomatoes.Checked = false;
            chkOnion.Checked = false;
            chkOlives.Checked = false;
            chkGreenPappers.Checked = false;

            //Reset Crust Type:
            rbThick.Checked = false;
            rbThin.Checked = true;

            //Reset Where To Eate : 
            rbEatIn.Checked = true;
            rbTakeOut.Checked = false;

            // Reset Order Pizza Button:
            btnOrderPizza.Enabled = true;

        }

        void UpdateOrderSummury()
        {
            UpdateSize();
            UpdateCrust();
            UpdateToppings();
            UpdateWhereToEat();
            UpdateTotalPrice();

        }
        private void rbSmall_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSize();
        }

        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSize();

        }

        private void rbLarge_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSize();

        }

        private void chkExtraaCheese_CheckedChanged(object sender, EventArgs e)
        {
            UpdateToppings();
        }

        private void chkMushrooms_CheckedChanged(object sender, EventArgs e)
        {
            UpdateToppings();
        }

        private void chkTomatoes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateToppings();
        }

        private void chkOnion_CheckedChanged(object sender, EventArgs e)
        {
            UpdateToppings();
        }

        private void chkOlives_CheckedChanged(object sender, EventArgs e)
        {
            UpdateToppings();
        }

        private void chkGreenPappers_CheckedChanged(object sender, EventArgs e)
        {
            UpdateToppings();
        }

        private void rbThick_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCrust();
        }

        private void rbThin_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCrust();
        }

        private void rbEatIn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateWhereToEat();
        }

        private void rbTakeOut_CheckedChanged(object sender, EventArgs e)
        {
            UpdateWhereToEat();
        }

        private void btnOrderPizza_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Order","Confirm",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                MessageBox.Show("Order Placed Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gbSize.Enabled = false;
                gbToppings.Enabled = false;
                gbCrustType.Enabled = false;
                gbWhereToEat.Enabled = false;
                btnOrderPizza.Enabled = false;
            }
        }

        private void btnResetFrom_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateOrderSummury();
        }
    }
}

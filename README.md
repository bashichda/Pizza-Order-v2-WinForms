# 🍕 Pizza Order App v2 — C# Windows Forms

![C#](https://img.shields.io/badge/Language-C%23-purple?style=flat-square&logo=csharp)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey?style=flat-square&logo=windows)
![Framework](https://img.shields.io/badge/Framework-.NET%204.7.2-blueviolet?style=flat-square)
![UI](https://img.shields.io/badge/UI-Windows%20Forms-blue?style=flat-square)
![Version](https://img.shields.io/badge/Version-2.0-gold?style=flat-square)
![Status](https://img.shields.io/badge/Status-Complete-brightgreen?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)

A fully refactored **single-form pizza ordering app** built with C#. v2 introduces a live order summary panel that updates in real time, Tag-based pricing, GroupBox form locking after order, and a proper Reset button — all in one clean screen.

> 🔗 **v1 (3-form version):** [Pizza-Order-v1](https://github.com/YourUsername/Pizza-Order-WinForms-CSharp)

---

## 📸 Preview

```
┌─────────────────────────────────────────────────────────────────┐
│                      🍕 Pizza Order                             │
│                                                                 │
│  ┌── Size ──────┐  ┌── Crust ─────┐  ┌── Where To Eat ──────┐  │
│  │ ○ Small  20$ │  │ ○ Thin    0$ │  │ ● Eat In          0$ │  │
│  │ ● Medium 30$ │  │ ● Thick  10$ │  │ ○ Take Out        0$ │  │
│  │ ○ Large  40$ │  └──────────────┘  └──────────────────────┘  │
│  └──────────────┘                                               │
│  ┌── Toppings (5$ each) ──────────────────────────────────────┐ │
│  │ ☑ Extra Cheese  ☐ Mushrooms  ☑ Tomatoes                   │ │
│  │ ☐ Onion         ☐ Olives     ☐ Green Peppers              │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌── Order Summary ────────────────────────────────────────┐    │
│  │  Size        : Medium                                   │    │
│  │  Crust       : Thick Crust                              │    │
│  │  Toppings    : Extra Cheese, Tomatoes                   │    │
│  │  Where       : Eat In                                   │    │
│  │  Total Price : 50$                                      │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                 │
│         [ 🍕 Order Pizza ]        [ 🔄 Reset ]                  │
└─────────────────────────────────────────────────────────────────┘
```

---

## ✨ What's New in v2

| Feature | v1 | v2 |
|---|---|---|
| Number of screens | 3 Forms | 1 Form ← |
| Live order summary panel | ❌ | ✅ |
| Tag-based pricing (no hardcoded if/else) | ❌ | ✅ |
| Prices stored on controls via `.Tag` | ❌ | ✅ |
| Form locking after order confirmation | ❌ | ✅ |
| Reset button with defaults | ❌ | ✅ |
| `UpdateOrderSummary()` central method | ❌ | ✅ |
| Default selections on load | ❌ | ✅ |

---

## 🗂️ Project Structure

```
Order_Pizza_v_2/
│
├── Program.cs            # Entry point → Application.Run(new Form1())
├── Form1.cs              # All logic — pricing, summary, reset, order confirm
├── Form1.Designer.cs     # UI layout with Tag-based prices on controls
└── README.md
```

---

## 🧱 Code Architecture

### Tag-Based Pricing — the key upgrade

In v1, prices were hardcoded inside every `CheckedChanged` event. In v2, prices live on the controls themselves via the `.Tag` property:

```csharp
// Designer sets Tag on each control:
rbSmall.Tag  = "20";
rbMedium.Tag = "30";
rbLarge.Tag  = "40";
rbThin.Tag   = "0";
rbThick.Tag  = "10";
chkExtraCheese.Tag = "5";
// ...

// Form1.cs reads Tag at calculation time:
float GetSelectedSizePrice() {
    if (rbSmall.Checked)   return Convert.ToSingle(rbSmall.Tag);
    if (rbMedium.Checked)  return Convert.ToSingle(rbMedium.Tag);
    return Convert.ToSingle(rbLarge.Tag);
}

float CalculateToppingsPrice() {
    float total = 0;
    if (chkExtraCheese.Checked) total += Convert.ToSingle(chkExtraCheese.Tag);
    if (chkMushrooms.Checked)   total += Convert.ToSingle(chkMushrooms.Tag);
    // ...
    return total;
}

float CalculateTotalPrice() {
    return GetSelectedSizePrice() + GetSelectedCrustPrice() + CalculateToppingsPrice();
}
```

> To change a price → update the `.Tag` in Designer. No logic code needs to change.

### Central Update Pattern

All `CheckedChanged` events delegate to one of four `Update*()` methods, which all call `UpdateTotalPrice()`:

```
rbSmall_CheckedChanged  ──► UpdateSize()    ──► UpdateTotalPrice()
chkOnion_CheckedChanged ──► UpdateToppings() ──► UpdateTotalPrice()
rbThick_CheckedChanged  ──► UpdateCrust()   ──► UpdateTotalPrice()
rbEatIn_CheckedChanged  ──► UpdateWhereToEat() ──► UpdateTotalPrice()
```

`UpdateOrderSummury()` calls all four at once — used on `Form_Load` to populate defaults.

### Form Locking After Order

```csharp
private void btnOrderPizza_Click(object sender, EventArgs e) {
    if (MessageBox.Show("Confirm Order", "Confirm",
        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
    {
        MessageBox.Show("Order Placed Successfully!");
        // Lock all GroupBoxes — prevent editing after order
        gbSize.Enabled      = false;
        gbToppings.Enabled  = false;
        gbCrustType.Enabled = false;
        gbWhereToEat.Enabled = false;
        btnOrderPizza.Enabled = false;
    }
}
```

### Reset to Defaults

```csharp
void ResetForm() {
    // Re-enable all groups
    gbSize.Enabled = gbToppings.Enabled =
    gbCrustType.Enabled = gbWhereToEat.Enabled = true;

    // Set defaults
    rbMedium.Checked = true;
    rbThin.Checked   = true;
    rbEatIn.Checked  = true;
    chkExtraaCheese.Checked = chkMushrooms.Checked =
    chkTomatoes.Checked = chkOnion.Checked =
    chkOlives.Checked = chkGreenPappers.Checked = false;

    btnOrderPizza.Enabled = true;
}
```

### Pricing Table

| Option | Price |
|---|---|
| Small | 20$ |
| Medium | 30$ |
| Large | 40$ |
| Thin Crust | 0$ |
| Thick Crust | 10$ |
| Each Topping | 5$ |
| Eat In | 0$ |
| Take Out | 0$ |

---

## 🚀 Getting Started

### Prerequisites
- **Visual Studio 2019+**
- **.NET Framework 4.7.2**
- Windows OS

### Run
1. Open `MyFirstWindowsForm.sln`
2. Press `Ctrl + F5`

---

## 🔮 Possible Improvements

- [ ] Loop through all CheckBoxes in `CalculateToppingsPrice()` instead of manual if-chain
- [ ] Add **quantity selector** (NumericUpDown) per pizza
- [ ] Save order to a **log file**
- [ ] Add **discount code** field
- [ ] Replace `float` with `decimal` for proper currency math

---

## 👨‍💻 Author

> Built with ❤️ as part of a C# Windows Forms learning journey.

Feel free to fork, star ⭐, or contribute!

---

## 📄 License

This project is licensed under the **MIT License** — free to use and modify.

<div align="center">

![Preview](docs/deskscribe.png)
### Draw. Doodle. Think.

A lightweight transparent overlay for Windows that lets you sketch ideas instantly using a global hotkey.


</div>

---

# 🚀 Overview

**DeskScribe** is a minimal, fast, distraction-free overlay tool for Windows.  
Press **Ctrl + D** anytime → a transparent whiteboard appears over your desktop.  
Draw, annotate, ideate — without switching apps or losing flow.

Perfect for:
- Thinking out loud
- Planning workflows
- Quick math and outlines
- Coding/architecture notes
- Teaching, explaining, screen recording

---

# ✨ Features (v1.0.0)

### Core Drawing
- Transparent fullscreen overlay
- Freehand drawing
- Brush color cycle — **Ctrl + K**
- Brush thickness **Ctrl + +** and **Ctrl + -**
- Undo last stroke — **Ctrl + Z**
- Clear canvas — **Ctrl + C**

### Saving & Loading
- Save canvas as PNG — **Ctrl + S**
- Load last saved image — **Ctrl + O**
- Set saved PNG as wallpaper — **Ctrl + B**

### System-Level Features
- Global hotkey → **Ctrl + D** opens DeskScribe from anywhere
- Runs from system tray (show/hide/exit)
- Autostarts with Windows
- Installer included (via Inno Setup)

---

# 📦 Download

➡️ **Download the latest version:**  
https://github.com/Ashisane/DeskScribe/releases/latest

You’ll find:
- `DeskScribe-Setup.exe`

---

# 🛠 Installation

### Option 1 — Installer (recommended)
1. Download `DeskScribe-Setup.exe`
2. Run installer
3. DeskScribe will appear in your system tray
4. Press **Ctrl + D** to open the overlay

---

# 🎨 Shortcuts

| Action | Shortcut |
|--------|----------|
| Toggle overlay | **Ctrl + D** |
| Save PNG | **Ctrl + S** |
| Load last PNG | **Ctrl + O** |
| Set as wallpaper | **Ctrl + B** |
| Undo | **Ctrl + Z** |
| Clear canvas | **Ctrl + C** |
| Change brush color | **Ctrl + K** |
| Brush size + | **Ctrl + +** |
| Brush size - | **Ctrl + -** |
| Hide overlay | **Esc** |

## Tech Stack

- **Language:** C# (.NET 9.0)
- **UI Framework:** WPF (Windows Presentation Foundation)
- **IDE:** JetBrains Rider
- **Build Tooling:** dotnet CLI
- **Testing:** MSTest
- **Future Modules:** Native C++ Direct2D renderer (for hardware-accelerated drawing)

---

## Project Structure

```
DeskScribe/
├── src/
│   └── DeskScribe.App/          # Main WPF overlay application
├── tests/
│   └── DeskScribe.App.Tests/    # Future test suite
├── docs/
│   └── preview.png              # Optional README preview
├── .github/
│   ├── PULL_REQUEST_TEMPLATE.md
│   └── workflows/               # Future CI
├── .gitignore
├── CONTRIBUTING.md
├── LICENSE
└── README.md
```

---

## Getting Started

### Requirements
- Windows 10 or later
- .NET 9.0 SDK
- JetBrains Rider / Visual Studio / VS Code with C# Dev Kit

### Clone & Run

```bash
git clone https://github.com/<your-username>/DeskScribe.git
cd DeskScribe

# Restore
dotnet restore

# Run the overlay app
dotnet run --project src/DeskScribe.App
```

---

## Usage

| Shortcut | Action |
|---------|--------|
| **Alt + D** | Toggle draw/view mode |
| **Ctrl + S** | Save canvas as transparent PNG |
| **Ctrl + O** | Load last saved PNG |
| **Ctrl + B** | Set saved PNG as desktop wallpaper |
| **Ctrl + C** | Clear canvas (strokes + background) |
| **Ctrl + K** | Cycle brush colors |
| **Ctrl + + / Ctrl + -** | Change brush thickness |
| **Ctrl + Z** | Undo last stroke |
| **Esc** | Close overlay |

Saved images are located at:

```
Pictures/DeskScribe/
```

App config (last saved image path):

```
AppData/Roaming/DeskScribe/config.json
```

---

# 🗺 Roadmap

- v1.1.x — Settings window
- v1.2.x — Color picker + eraser
- v1.3.x — Multi-screen support
- v2.x — Vector engine + persistent projects

---

# 🐞 Reporting Issues

Found a bug?  
Open an issue here:  
https://github.com/Ashisane/DeskScribe/issues

Please include:
- Steps to reproduce
- Expected vs actual behavior
- Screenshot (if relevant)
- Windows version

---

## Contributing

Contributions are welcome!

Track development progress and feature planning on the official Trello board:  
👉 [DeskScribe Trello Board](https://trello.com/invite/b/69118155c62d6d5c7bcd7585/ATTI9f038cc9933116a34f82600d423355da427587DF/deskscribe)

1. Fork the repo
2. Create a feature branch (`git checkout -b feature/awesome-idea`)
3. Commit your changes (`git commit -m "feat: add awesome idea"`)
4. Push to your branch (`git push origin feature/awesome-idea`)
5. Open a Pull Request

For details, see [CONTRIBUTING.md](CONTRIBUTING.md).

---

## License

Distributed under the **MIT License**.  
See [LICENSE](LICENSE) for details.

---

## Acknowledgments

- [JetBrains Rider](https://www.jetbrains.com/rider/) — for making C# development a joy.
- [Microsoft .NET SDK](https://dotnet.microsoft.com/) — for an awesome dev ecosystem.
- Everyone who loves to sketch while they think.

---

<div align="center">

Made with focus, coffee, and curiosity.  
Contributors welcome → [issues](https://github.com/<your-username>/DeskScribe/issues)

</div>

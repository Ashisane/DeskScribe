<div align="center">

# DeskScribe
### Draw. Doodle. Think — directly on your desktop.

A lightweight transparent overlay that lets you sketch, annotate, and capture ideas instantly without switching apps.

</div>

---

## Features (v0.3.0)

- Fullscreen transparent drawing overlay
- Adjustable brush colors (Ctrl + K)
- Adjustable brush thickness (Ctrl + + / Ctrl + -)
- Undo last stroke (Ctrl + Z)
- Clear entire board (Ctrl + C)
- Save your drawing as a transparent PNG (Ctrl + S)
- Load your last saved sketch (Ctrl + O)
- Set drawing as desktop wallpaper (Ctrl + B)
- Close overlay instantly (Esc)

DeskScribe is designed to stay out of your way and be available exactly when you need it.

---

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

## Roadmap

| Version | Features | Status |
|---------|----------|--------|
| **v0.1.0** | Transparent overlay + drawing | Done |
| **v0.2.0** | Colors, brush size, undo, clear | Done |
| **v0.3.0** | Save, load, set wallpaper | Done |
| **v0.4.0** | System tray, global hotkeys, auto-start | Planned |
| **v1.0.0** | Vector stroke files, full stroke persistence | Planned |
| **Future** | Direct2D renderer, cloud sync | Ideas |

See [docs/architecture.md](docs/architecture.md) for module-level breakdowns.

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

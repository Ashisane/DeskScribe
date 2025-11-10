<div align="center">

# DeskScribe
### Draw. Doodle. Think — right on your desktop.

![DeskScribe Preview](docs/preview.png)

**DeskScribe** is a lightweight, always-available desktop overlay that lets you draw or jot down ideas directly on your Windows desktop — no need to open any app or lose your flow.

</div>

---

## Features

- Persistent overlay — write or sketch directly on your desktop wallpaper.
- Smooth pen drawing with adjustable color & brush size.
- Ultra-lightweight — optimized for minimal CPU and RAM use.
- Autosave your doodles between sessions.
- Quick toggle (e.g., Alt + D) to enter draw mode instantly.
- Export your notes as `.png` or set as wallpaper.
- Auto-start on login — available the moment you unlock your screen.
- Open source & modular — clean architecture designed for future expansion.

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
│   ├── DeskScribe.App/         # WPF front-end and main window overlay
│   └── DeskScribe.Core/        # Core logic: drawing, strokes, persistence
├── tests/
│   └── DeskScribe.Core.Tests/  # Unit tests for core logic
├── docs/
│   ├── preview.png             # Screenshot for README
│   └── architecture.md         # Optional deeper documentation
├── .github/
│   └── workflows/              # CI/CD pipelines
├── .gitignore
├── .editorconfig
├── LICENSE
└── README.md
```

---

## Getting Started

### Prerequisites
- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [JetBrains Rider](https://www.jetbrains.com/rider/)
- Windows 10 or later

### Clone & Build

```bash
# Clone repository
git clone https://github.com/<your-username>/DeskScribe.git
cd DeskScribe

# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run the app (WPF project)
dotnet run --project src/DeskScribe.App
```

---

## Usage

Once the app starts:

- Press **Alt + D** to toggle draw mode.
- Draw anywhere on the desktop using your mouse or stylus.
- Press **Esc** or toggle again to exit draw mode.
- Use the system tray icon to *Save*, *Clear*, or *Exit*.

All your strokes are automatically saved between sessions and reloaded when you log back in.

---

## Roadmap

| Phase | Feature | Status      |
|-------|----------|-------------|
| 1     | Transparent overlay & draw mode toggle | In Progress |
| 2     | System tray integration | Planned     |
| 3     | Stroke persistence & save as PNG | Planned     |
| 4     | Auto-start & unlock event handling | Planned     |
| 5     | Direct2D native rendering engine | Future      |
| 6     | Cloud sync / collaborative notes | Idea        |

See [docs/architecture.md](docs/architecture.md) for module-level breakdowns.

---

## Running Tests

```bash
dotnet test
```

Tests live under `/tests` and follow the MSTest convention.

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

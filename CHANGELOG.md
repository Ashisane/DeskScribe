# Changelog
All notable changes to this project will be documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)  
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.0.0] - 2025-01-01
### Added
- Transparent fullscreen whiteboard overlay
- Global hotkey: **Ctrl + D** to show overlay
- Brush controls:
    - Cycle brush color (**Ctrl + K**)
    - Increase/decrease size (**Ctrl + +/-**)
    - Undo stroke (**Ctrl + Z**)
    - Clear canvas (**Ctrl + C**)
- Save canvas to PNG (**Ctrl + S**)
- Load last saved image (**Ctrl + O**)
- Set saved PNG as wallpaper (**Ctrl + B**)
- System tray integration (show/hide/exit)
- Auto-start at login
- Portable build and full Windows installer (Inno Setup)

### Fixed
- Canvas not accepting input when opened from startup
- Global hotkey reliability issues
- Tray icon disappearing
- Startup window auto-closing unexpectedly

### Notes
- This is the first stable release of DeskScribe.
- Core functionality complete and optimized for desktop workflow.

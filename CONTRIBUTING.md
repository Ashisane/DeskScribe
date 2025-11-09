# Contributing to DeskScribe

Thank you for your interest in contributing! 🎉  
DeskScribe is an open-source project, and every contribution helps make it better.

---

## Getting Started

### 1. Fork and Clone

```bash
git clone https://github.com/<your-username>/DeskScribe.git
cd DeskScribe
```

### 2. Create a Branch

Always create a new branch for your changes:

```bash
git checkout -b feature/<feature-name>
# or for bug fixes
git checkout -b fix/<bug-name>
```

---

## Commit Messages

Follow the [Commit Message Guidelines](docs/commits.md).  
We use the **Conventional Commits** convention.

**Examples:**
```
feat: add transparent overlay window
fix: correct drawing stroke alignment
docs: update usage instructions
```

---

## Pull Requests

When you’re ready to submit changes:

1. Push your branch:
   ```bash
   git push origin feature/<feature-name>
   ```
2. Open a Pull Request (PR) to the `dev` branch.
3. Include a clear title and description of what you changed.
4. Ensure all tests pass (`dotnet test`).

---

## Code Style

- Follow C# conventions (`PascalCase` for methods, `camelCase` for fields).
- Keep code self-explanatory and add comments for complex logic.
- Use Rider’s code formatting tools before committing.

---

## Directory Structure

```
src/   -> Application and core libraries
tests/ -> Unit and integration tests
docs/  -> Documentation and developer resources
```

---

## Branch Naming Convention

| Branch Type | Example | Purpose |
|--------------|----------|----------|
| `feature/` | `feature/overlay-toggle` | New feature or enhancement |
| `fix/` | `fix/mouse-event-bug` | Bug fixes |
| `docs/` | `docs/readme-update` | Documentation changes |
| `chore/` | `chore/update-ci` | Maintenance or config changes |

---

## Testing

Before submitting a PR, always ensure your code builds and tests pass:

```bash
dotnet build
dotnet test
```

---

## Reporting Issues

If you find a bug or have a feature request:
1. Search [existing issues](https://github.com/<your-username>/DeskScribe/issues).
2. If not found, open a **new issue** describing:
    - Steps to reproduce
    - Expected vs actual behavior
    - Environment info (OS, .NET version, etc.)

---

## License

By contributing, you agree that your contributions will be licensed under the [MIT License](../LICENSE).

---

Thanks again for making DeskScribe better! 🙌

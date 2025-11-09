# Commit Message Guidelines

This project follows the **Conventional Commits** standard for clear, consistent, and automation-friendly commit messages.

---

## Format

Each commit message must follow this pattern:

```
<type>(optional scope): <short summary>
```

### Examples
```
feat: add transparent overlay window
fix: prevent overlay from blocking desktop icons
chore: update .gitignore for Rider cache
docs: add README and CONTRIBUTING guide
refactor(core): optimize drawing stroke storage
test(core): add unit tests for stroke serialization
```

---

## Allowed Types

| Type | Purpose |
|------|----------|
| **feat** | Introduces a new feature |
| **fix** | Fixes a bug or issue |
| **chore** | Maintenance, configs, setup, version bumps |
| **docs** | Documentation-only changes |
| **style** | Formatting, naming, or style fixes (no logic) |
| **refactor** | Code restructuring without behavior change |
| **test** | Adds or modifies tests |
| **build** | Build process or dependency updates |
| **ci** | CI/CD configuration updates |

---

## Writing Good Summaries

- Use **imperative mood** (e.g., “add overlay,” not “added overlay”).
- Keep it **under 72 characters**.
- Do **not** end with a period.
- Capitalize the first letter.
- Include a **scope** in parentheses if relevant:  
  `feat(core): implement stroke persistence`.

---

## Benefits

- Easier to understand history at a glance.
- Enables automated changelogs and semantic versioning.
- Promotes professionalism and contributor clarity.

---

## Example Workflow

```bash
git add .
git commit -m "feat: add brush color picker"
git push -u origin feature/color-picker
```

---

## References

- [Conventional Commits Specification](https://www.conventionalcommits.org/en/v1.0.0/)
- [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
- [Semantic Versioning](https://semver.org/)

# Contributing to GamblingBot

Thank you for your interest in contributing to GamblingBot! We welcome contributions from developers of all skill levels.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/your-username/gambeling.git`
3. Create a branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Commit: `git commit -am 'Add your feature'`
6. Push: `git push origin feature/your-feature-name`
7. Submit a Pull Request

## Development Setup

### Requirements
- .NET 8.0 SDK or later
- Visual Studio or VS Code with C# extension

### Build & Run

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build

# Run
dotnet run
```

## Code Style

- Follow C# naming conventions (PascalCase for public members)
- Use meaningful variable and method names
- Add comments for complex logic
- Keep methods focused and concise

## Testing

Before submitting a PR, please:
- Test all command interactions
- Verify balance persistence
- Check error handling
- Ensure no console errors

## Commit Messages

Use clear, descriptive commit messages:
- ✨ `feat: Add new command`
- 🐛 `fix: Resolve balance calculation bug`
- 📚 `docs: Update README`
- 🧹 `chore: Update dependencies`

## Issues & Bug Reports

Found a bug? Please open an issue with:
- Clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable

## Feature Requests

Have an idea? Open an issue describing:
- The feature you'd like
- Why it would be useful
- Potential implementation approach

---

Thank you for making GamblingBot better!

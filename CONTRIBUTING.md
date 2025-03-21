
# Contributing

We appreciate your interest in contributing to the project! To make the contribution process smooth and efficient, please follow the guidelines outlined below.

## How to Contribute

1. **Fork the Repository**  
   Start by forking the repository on GitHub. This creates a personal copy of the repository where you can freely make changes.

2. **Create a New Branch**  
   It’s essential to work on a separate branch instead of directly on the `main` or `master` branch. Create a new branch for the changes you intend to make.
   ```bash
   git checkout -b feature-name
   ```

3. **Make Your Changes**  
   Make your changes, and ensure that your code adheres to the existing code style. If you are adding a new feature, ensure that it is well documented. If you are fixing a bug, ensure that the bug is thoroughly addressed and tested.

4. **Commit Your Changes**  
   Once you're happy with your changes, commit them to your branch. Write clear and concise commit messages that explain the changes you made.
   ```bash
   git commit -m "Your clear and concise commit message"
   ```

5. **Push Changes to Your Fork**  
   Push your changes to the repository on GitHub.
   ```bash
   git push origin feature-name
   ```

7. **Submit a Pull Request (PR)**  
   Once your changes are pushed to your fork, go to the original repository and submit a pull request (PR). Make sure to describe the purpose of your changes, and link any relevant issues if applicable.

## Code Style and Formatting

To ensure a clean and consistent codebase, please follow the guidelines below when contributing:

- **Naming Conventions**  
  Use PascalCase for class names, method names, and properties. Use camelCase for local variables and parameters

- **Indentation**  
  Use consistent indentation

- **Line Length**  
  Keep line length short for better readability

- **Commenting and Documentation**  
  - Document all methods and classes with XML comments.
    
    Example:
    ```csharp
    /// <summary>
    /// Divides two numbers.
    /// </summary>
    /// <param name="a">The numerator.</param>
    /// <param name="b">The denominator.</param>
    /// <returns>The result of dividing <paramref name="a"/> by <paramref name="b"/>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="b"/> is zero.</exception>
    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Denominator cannot be zero.");
        }
        return a / b;
    }
    ```
  - Write clear and concise comments for complex logic or functions.
  - Use comments to explain the purpose of non-obvious code, but avoid obvious comments.

## Pull Request Review

After submitting your pull request (PR):

1. A maintainer will review your code.
2. They may suggest improvements or request changes.
3. Once the PR is approved, it will be merged into the main branch.

## License

By submitting a contribution, you agree that your code is provided under the project’s [LICENSE](./LICENSE).

## Acknowledgements

Thanks again for contributing! We value your time and effort to improve the project. Together, we can make it better!

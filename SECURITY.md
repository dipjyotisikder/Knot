# Security Policy

## Supported Versions

We release patches for security vulnerabilities for the following versions:

| Version | Supported |
| ------- | --------- |
| 1.0.x   | Yes       |
| < 1.0   | No        |

## Reporting a Vulnerability

The Knot team takes security bugs seriously. We appreciate your efforts to responsibly disclose your findings.

### How to Report a Security Vulnerability

**Please do NOT report security vulnerabilities through public GitHub issues.**

Instead, please report them via email to [dipjyotisikder.dev@gmail.com](mailto:dipjyotisikder.dev@gmail.com).

Include as much of the following information as possible:

-   **Type of issue** (e.g., buffer overflow, SQL injection, cross-site scripting, etc.)
-   **Full paths of source file(s)** related to the manifestation of the issue
-   **Location of the affected source code** (tag/branch/commit or direct URL)
-   **Step-by-step instructions** to reproduce the issue
-   **Proof-of-concept or exploit code** (if possible)
-   **Impact of the issue**, including how an attacker might exploit it

This information will help us triage your report more quickly.

### What to Expect

-   **Acknowledgment**: You should receive an acknowledgment of your report within 48 hours.
-   **Initial Assessment**: We will provide an initial assessment of the vulnerability within 5 business days.
-   **Regular Updates**: We will keep you informed about our progress towards a fix.
-   **Resolution**: Once the vulnerability is resolved, we will notify you and publicly disclose the issue (giving you credit if desired).

### Our Commitment

-   We will respond to your report within 48 hours with our evaluation and expected resolution date
-   We will handle your report with strict confidentiality and will not pass on your personal details to third parties without your permission
-   We will keep you informed of the progress towards resolving the problem
-   We will credit you as the reporter (if desired) when we announce the vulnerability

## Disclosure Policy

When we receive a security bug report, we will:

1. Confirm the problem and determine the affected versions
2. Audit code to find any similar problems
3. Prepare fixes for all supported versions
4. Release new versions as soon as possible

## Security Update Process

1. Security fix is prepared in a private repository
2. New version is released with security patch
3. Security advisory is published on GitHub
4. Users are notified through:
    - GitHub Security Advisories
    - Release notes
    - NuGet package update

## Security Best Practices for Users

When using Knot in your applications:

1. **Keep Updated**: Always use the latest version of Knot
2. **Validate Input**: Validate data before mapping, especially from untrusted sources
3. **Review Configurations**: Regularly audit your mapping configurations
4. **Monitor Dependencies**: Keep your application's dependencies up to date
5. **Error Handling**: Implement proper error handling for mapping operations

## Known Security Considerations

### Type Safety

Knot uses reflection and compiled expressions for mapping. Be aware that:

-   Mapping configurations are validated at configuration time
-   Type mismatches are caught during configuration or first mapping execution
-   No arbitrary code execution is possible through mapping configurations

### Data Exposure

When mapping entities to DTOs:

-   Always explicitly configure what properties to map
-   Use `Ignore()` for sensitive properties (passwords, tokens, etc.)
-   Review profiles to ensure no sensitive data leaks

Example of secure configuration:

```csharp
cfg.CreateMap<User, UserDto>(map =>
{
    map.Ignore(dest => dest.PasswordHash);
    map.Ignore(dest => dest.SecurityToken);
    map.Ignore(dest => dest.InternalNotes);
});
```

### Expression Compilation

Knot compiles LINQ expressions for performance:

-   Expressions are compiled from trusted configuration code only
-   No user input is used in expression compilation
-   Expression cache is internal and immutable after creation

## Vulnerability Disclosure Timeline

-   **Day 0**: Security report received
-   **Day 1-2**: Acknowledgment sent to reporter
-   **Day 3-7**: Initial assessment and triage
-   **Day 8-30**: Fix development and testing
-   **Day 31-45**: Patch release and public disclosure

## Bug Bounty Program

We currently do not have a bug bounty program.

However, we deeply appreciate security researchers who report vulnerabilities responsibly and will acknowledge their contributions in our release notes and security advisories (with permission).

## Comments on This Policy

If you have suggestions on how this process could be improved, please submit a pull request or open an issue.

## Contact

For security-related questions or concerns, please contact:

-   **Email**: dipjyotisikder.dev@gmail.com

---

**Last Updated**: November 2025

Thank you for helping keep Knot and its users safe!

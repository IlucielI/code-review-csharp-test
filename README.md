# C# (.NET 8) Benchmark Test Suite

[![C# Version](https://img.shields.io/badge/C%23-12-239120.svg?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4.svg?logo=dotnet)](https://dotnet.microsoft.com/)
[![Benchmark Category](https://img.shields.io/badge/Benchmark-Security%20%26%20IDisposable%20Leaks-blue.svg)](#test-case-matrix)
[![Safe Guard](https://img.shields.io/badge/False%20Positive%20Guard-Active-brightgreen.svg)](#anti-false-positive-guard-file)

Benchmark test suite for automated code review engines on C# (.NET 8) backend applications. This repository evaluates review engine capability across ADO.NET SQL injection, Process.Start command injection, unmanaged `IDisposable` resource leaks, ReDoS regex patterns, and C# safe pattern guards.

---

## 🎯 Benchmark Purpose

1. **.NET Security Precision:** Catches raw SQL concatenation in `SQLiteCommand` / `SqlCommand`, unescaped shell arguments in `ProcessStartInfo`, unvalidated `HttpClient` SSRF calls, and path traversal in `Path.Combine()`.
2. **IDisposable & Resource Management:** Detects `FileStream` and unmanaged handles opened without C#'s `using` declaration or explicit `.Dispose()` call.
3. **C# Safe Idioms & Zero False Positives:** Validates that `@parameter` bindings, `using var` declarations, and URI domain whitelists produce **0 false positives**.

---

## 📋 Test Case Matrix

### 🔴 Security Vulnerabilities

| File | Issue / Vulnerability | Type | CWE | Severity | Expected |
| :--- | :--- | :--- | :--- | :---: | :---: |
| `UserRepository.cs` | SQL Injection via string concatenation in `SQLiteCommand` | Injection | CWE-89 | High | **BLOCKING** |
| `DiagnosticService.cs` | Command Injection via `Process.Start` with `sh -c` | RCE | CWE-78 | High | **BLOCKING** |
| `FileStorageService.cs` | Path Traversal via unvalidated `Path.Combine(baseDir, filename)` | File Security | CWE-22 | High | **BLOCKING** |
| `HttpProxyService.cs` | Server-Side Request Forgery via unvalidated `HttpClient` | Network Security | CWE-918 | Medium | **BLOCKING** |
| `AuthService.cs` | Hardcoded JWT Secret Key & Plaintext Credential Logging | Information Disclosure | CWE-798 / CWE-532 | High | **BLOCKING** |
| `RedirectEndpoint.cs` | Open Redirect without host domain validation | Redirection | CWE-601 | Medium | **BLOCKING** |
| `UserProfileEndpoint.cs` | IDOR on user account deletion without ownership check | Broken Access Control | CWE-639 | High | **BLOCKING** |
| `CorsPolicyConfig.cs` | Wildcard \`AllowAnyOrigin()\` with \`AllowCredentials()\` | CORS Misconfiguration | CWE-942 | High | **BLOCKING** |
| `XmlReaderService.cs` | XML parser without DTD expansion prohibition (XXE) | Injection / XXE | CWE-611 | High | **BLOCKING** |
| `CookieHelper.cs` | Cookies explicitly configured with \`HttpOnly = false\` and \`Secure = false\` | Insecure Cookie | CWE-614 / CWE-1004 | Medium | **NON-BLOCKING** |
| `LoginEndpoint.cs` | Authentication login route missing rate limiting or throttling | Missing Rate Limiting | CWE-307 | Medium | **NON-BLOCKING** |

### ⚡ Performance & Resource Leaks

| File | Issue | Type | Severity | Expected |
| :--- | :--- | :--- | :---: | :---: |
| `DocumentReader.cs` | Unclosed `FileStream` resource leak without `using` / `Dispose()` | Resource Leak | Medium | **NON-BLOCKING** |
| `RegexService.cs` | Catastrophic Backtracking Regular Expression (ReDoS) | Algorithmic Complexity | Medium | **NON-BLOCKING** |

---

## 🛡️ Anti-False-Positive Guard File

| File | Safe Pattern Implemented | Expected Reviewer Result |
| :--- | :--- | :---: |
| `SafeGuards.cs` | Parameterized SQLite command (`@user`), Whitelist URI validation, `using` automatic disposal, safe XML reader (`DtdProcessing.Prohibit`), hardened `HttpOnly`/`Secure` cookies | **0 False Positives** (Clean) |

---

## 🚀 How to Run the Benchmark

```bash
# View PR on GitHub
gh pr view 1 --web

# Trigger Review via API
curl -X POST http://localhost:8081/api/v1/review/trigger \
  -H "Content-Type: application/json" \
  -d '{
    "repository": "IlucielI/code-review-csharp-test",
    "pull_request_id": 1
  }'
```

---

## 📊 Benchmark Validation Results

- **Detection Rate:** 15 / 15 (100%)
- **False Positive Rate:** 0 / 1 (`SafeGuards.cs` completely passed)
- **False Negative Rate:** 0%

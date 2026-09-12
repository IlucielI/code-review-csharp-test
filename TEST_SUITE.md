# C# Benchmark Test Suite Documentation

Dokumentasi suite pengujian kerentanan keamanan dan performa pada C# (.NET 8).

## Daftar Test Case

| File | Kategori | Deskripsi Masalah | Tingkat Risiko |
| :--- | :--- | :--- | :--- |
| `UserRepository.cs` | Security | Raw SQL injection via string concatenation | High |
| `DiagnosticService.cs` | Security | Command injection via `Process.Start` with `sh -c` | High |
| `FileStorageService.cs` | Security | Path traversal via unvalidated `Path.Combine` | High |
| `HttpProxyService.cs` | Security | Server-Side Request Forgery via unvalidated `HttpClient` | Medium |
| `AuthService.cs` | Security | Hardcoded JWT secret key & plaintext credential logging | High |
| `RedirectEndpoint.cs` | Security | Open redirect without domain validation | Medium |
| `UserProfileEndpoint.cs` | Security | IDOR endpoint penghapusan user tanpa validasi auth / ownership | High |
| `RegexService.cs` | Performance | Catastrophic backtracking ReDoS regex pattern | Medium |
| `DocumentReader.cs` | Performance | Unclosed `FileStream` resource leak tanpa `using` / `Dispose()` | Medium |

## False-Positive Guard Files

| File | Pola Pengujian Guard | Ekspektasi Reviewer |
| :--- | :--- | :--- |
| `SafeGuards.cs` | Parameterized SQLite command (`@user`), Whitelist URI validation, `using` disposal | **0 False Positives** |

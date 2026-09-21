# MMF — Modular Monolith Framework · Yol Haritası

> Bu dosya, projenin **tek doğruluk kaynağıdır** (single source of truth).
> Tüm fazlar, issue'lar ve durumları burada takip edilir.

**Son güncelleme**: 2026-09-21

## Durum Simgeleri

| Simge | Anlam |
|---|---|
| ✅ | Tamamlandı (Done) |
| 🚧 | Aktif çalışılıyor (In Progress) |
| 👀 | Gözden geçiriliyor (Review) |
| ⏳ | Planlandı, başlanmadı (Todo) |
| 🔒 | Bağımlılık bekliyor (Blocked) |
| 💤 | Backlog (henüz sprint'e alınmadı) |

---

## Faz 1 — Core Module System 🚧

**Hedef**: Modül sistemi, DI, yaşam döngüsü ve temel altyapı.
**Süre**: 2 hafta (tahmini)
**Durum**: Devam ediyor

| # | Başlık | Durum | Öncelik | Efor | Bağımlılıklar |
|---|---|---|---|---|---|
| [#3](https://github.com/ofz-labs/mmf/issues/3) | [F1-01] Repo iskeleti, klasör yapısı ve proje standartları | 🚧 | Urgent | Medium | — |
| [#4](https://github.com/ofz-labs/mmf/issues/4) | [F1-02] Ortak build ve paket ayarları (CPM dahil) | ⏳ | Urgent | Low | F1-01 |
| [#5](https://github.com/ofz-labs/mmf/issues/5) | [F1-03] Ofz.Mmf.Core projesi ve temel soyutlamalar | ⏳ | High | Medium | F1-01, F1-02 |
| [#6](https://github.com/ofz-labs/mmf/issues/6) | [F1-04] MmfModule abstract sınıfı | ⏳ | High | Low | F1-03 |
| [#7](https://github.com/ofz-labs/mmf/issues/7) | [F1-05] DependsOnAttribute | ⏳ | High | Low | F1-04 |
| [#8](https://github.com/ofz-labs/mmf/issues/8) | [F1-06] ModuleLoader + topolojik sıralama | ⏳ | Urgent | High | F1-05 |
| [#9](https://github.com/ofz-labs/mmf/issues/9) | [F1-07] Context sınıfları | ⏳ | High | Medium | F1-04 |
| [#10](https://github.com/ofz-labs/mmf/issues/10) | [F1-08] AddMmf<T>() / UseMmf() DI extension'ları | ⏳ | Urgent | Medium | F1-06, F1-07 |
| [#11](https://github.com/ofz-labs/mmf/issues/11) | [F1-09] Örnek host uygulaması (3 modül) | ⏳ | High | Medium | F1-08 |
| [#12](https://github.com/ofz-labs/mmf/issues/12) | [F1-10] Unit testler (modül yükleme sırası) | ⏳ | High | High | F1-06, F1-08 |
| [#13](https://github.com/ofz-labs/mmf/issues/13) | [F1-11] GitHub Actions CI (build + test) | ⏳ | High | Medium | F1-02 |
| [#14](https://github.com/ofz-labs/mmf/issues/14) | [F1-12] README ve mimari dokümantasyonu | ⏳ | Medium | Medium | F1-09 |

**Faz 1 Çıktısı**: Çalışan, test edilmiş, dokümante edilmiş bir modül sistemi. 3 örnek modül ile demo.

---

## Faz 2 — Persistence 💤

**Hedef**: EF Core soyutlamaları, audit interceptor, soft-delete, çok kiracılı filtreler.
**Durum**: Planlanmadı (Faz 1 bittikten sonra detaylandırılacak)
**Tahmini Süre**: 3 hafta

**Planlanan Kapsam**:
- `Ofz.Mmf.Persistence` (soyutlamalar, EF Core bağımsız)
- `Ofz.Mmf.Persistence.EntityFrameworkCore` (implementasyon)
- `MmfDbContext`, `AddMmfDbContext<T>()`
- `AuditedEntityInterceptor` (CreatedBy, ModifiedBy otomatik doldurma)
- `SoftDeleteInterceptor` (DELETE → IsDeleted = true)
- `ApplyMmfConventions` (otomatik sorgu filtreleri)
- Çok kiracılı (multi-tenant) sorgu filtresi

---

## Faz 3 — Security 💤

**Hedef**: `ICurrentUserService`, JWT Bearer kimlik doğrulama, yetkilendirme.
**Durum**: Planlanmadı
**Tahmini Süre**: 3 hafta

**Planlanan Kapsam**:
- `Ofz.Mmf.Users` — `ICurrentUserService` soyutlaması
- `Ofz.Mmf.Authentication.JwtBearer` — temel JWT entegrasyonu
- `Ofz.Mmf.Authorization` — permission checker, policy desteği
- Sağlayıcıya özel modüller (Keycloak, Entra ID) — opsiyonel

---

## Faz 4+ — Planlanan Modüller 💤

İhtiyaç sırasına göre açılacak:

| Faz | Modül | Kapsam |
|---|---|---|
| Faz 4 | CQRS & Messaging | Wolverine, transactional outbox, handler keşfi |
| Faz 5 | Web & API | Minimal API entegrasyonu, endpoint keşfi, ProblemDetails |
| Faz 6 | Background Jobs | Hangfire/Quartz benzeri arka plan iş altyapısı |
| Faz 7 | Multi-tenancy | Tenant izolasyonu, tenant-aware servisler |
| Faz 8 | Observability & Packaging | OpenTelemetry, health checks, NuGet publish |
| Faz 9+ | İhtiyaca göre | Blob Storage, Notifications, Workflow, AI... |

---

## Referanslar

- **GitHub Issues**: https://github.com/ofz-labs/mmf/issues
- **Milestones**: https://github.com/ofz-labs/mmf/milestones
- **Project Board**: https://github.com/orgs/ofz-labs/projects
- **Granit Framework** (ilham kaynağı): https://github.com/granit-fx/granit-dotnet

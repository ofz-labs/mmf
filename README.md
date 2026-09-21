# MMF — Modular Monolith Framework

> .NET için sıfırdan yazılmış, modüler bir framework. Granit Framework'ün mimarisinden ilham alır.

[![Build Status](https://github.com/ofz-labs/mmf/actions/workflows/ci.yml/badge.svg)](https://github.com/ofz-labs/mmf/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

## 🎯 Nedir?

MMF (**M**odular **M**onolith **F**ramework), .NET uygulamalarını **modüler monolit** mimarisiyle kurmak için tasarlanmış bir framework'tür. Her modül:
- Kendi bağımlılıklarını `[DependsOn]` ile beyan eder
- Framework tarafından **topolojik sırayla** yüklenir
- İki aşamalı yaşam döngüsüne sahiptir (`ConfigureServices` → `OnApplicationInitialization`)

## 💡 Neden?

Mikroservis mimarisinin operasyonel karmaşıklığı ile monolitin sınırlarının sıkıştığı yerde, **modüler monolit** orta yolu sunar: tek deployment, ama içeride net sınırlar. MMF, bu yaklaşımı .NET'te uygulamak için gerekli altyapıyı sağlar.

## 🚧 Durum

Proje **aktif geliştirme aşamasında**. Faz 1 (Core Module System) üzerinde çalışılıyor. Detaylar için [ROADMAP](docs/ROADMAP.md)'e bakın.

## 🏗️ Mimari

```
┌─────────────────────────────────────────────────┐
│              Uygulama (Host)                    │
│                                                 │
│  builder.Services.AddMmf<AppModule>(config)     │
│                       │                         │
│                       ▼                         │
│            ┌────────────────────┐               │
│            │   ModuleLoader     │               │
│            │   (Kahn algoritması)│              │
│            └────────────────────┘               │
│                       │                         │
│                       ▼                         │
│   ┌──────────┬──────────┬──────────┐            │
│   │ Core     │ ModuleA  │ ModuleB  │            │
│   │ Module   │          │          │            │
│   └──────────┴──────────┴──────────┘            │
│                                                 │
│  await app.UseMmfAsync()                        │
│    → OnApplicationInitialization sırayla        │
└─────────────────────────────────────────────────┘
```

## 🚀 Kullanım (Yakında)

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMmf<AppModule>(builder.Configuration);

var app = builder.Build();
await app.UseMmfAsync();
await app.RunAsync();
```

Yeni modül yazmak için:

```csharp
[DependsOn(typeof(OtherModule))]
public class MyModule : MmfModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IMyService, MyService>();
    }
}
```

## 📦 Paketler

| Paket | Durum | Açıklama |
|---|---|---|
| `Ofz.Mmf.Core` | 🚧 Geliştiriliyor | Modül sistemi, DI, yaşam döngüsü |
| `Ofz.Mmf.Persistence` | ⏳ Planlandı | EF Core entegrasyonu |
| `Ofz.Mmf.Security` | ⏳ Planlandı | Kimlik doğrulama ve yetkilendirme |

> **Not**: Paketler henüz NuGet'te yayınlanmadı. Yayınlandığında bu bölüm güncellenecektir.

## 🛠️ Geliştirme

### Gereksinimler
- .NET SDK 10.0.400+
- Git 2.40+

### Kurulum

```bash
git clone https://github.com/ofz-labs/mmf.git
cd mmf
dotnet restore Ofz.Mmf.slnx
dotnet build Ofz.Mmf.slnx
dotnet test Ofz.Mmf.slnx
```

## 🗺️ Yol Haritası

Detaylı yol haritası için [docs/ROADMAP.md](docs/ROADMAP.md).

- ✅ **Faz 1** — Core Module System *(devam ediyor)*
- ⏳ **Faz 2** — Persistence (EF Core)
- ⏳ **Faz 3** — Security (JWT, ICurrentUserService)
- ⏳ **Faz 4+** — CQRS, Web API, Background Jobs, Multi-tenancy, Observability

## 🤝 Katkı Sağlama

Katkılar memnuniyetle karşılanır! Lütfen önce [CONTRIBUTING.md](CONTRIBUTING.md) dosyasını okuyun.

- 🐛 [Hata bildir](https://github.com/ofz-labs/mmf/issues/new?template=bug.yml)
- 🚀 [Özellik öner](https://github.com/ofz-labs/mmf/issues/new?template=feature.yml)

## 📄 Lisans

Bu proje [MIT lisansı](LICENSE) altında lisanslanmıştır.

## 🙏 Teşekkür

Bu proje, [Granit Framework](https://github.com/granit-fx/granit-dotnet)'ün mimarisinden ilham almıştır.

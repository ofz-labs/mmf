# Katkı Rehberi

MMF — Modular Monolith Framework'e katkı sağlamak istediğiniz için teşekkürler! Bu rehber, katkı sürecini baştan sona açıklar.

## 📋 İçindekiler

- [Davranış Kuralları](#davranış-kuralları)
- [Başlamadan Önce](#başlamadan-önce)
- [Geliştirme Ortamı Kurulumu](#geliştirme-ortamı-kurulumu)
- [Branch Stratejisi](#branch-stratejisi)
- [Commit Mesajları](#commit-mesajları)
- [Pull Request Süreci](#pull-request-süreci)
- [Kod Stili](#kod-stili)
- [Test Yazma](#test-yazma)

## Davranış Kuralları

Bu proje, katkı sağlayan herkese saygılı bir ortam sunmayı taahhüt eder. Yapıcı eleştiri memnuniyetle karşılanır, kişisel saldırılar kabul edilmez.

## Başlamadan Önce

1. **Issue açın**: Üzerinde çalışmak istediğiniz konu için önce bir issue açın (veya mevcut bir issue'yu seçin). Bu, çalışmanızın projeyle uyumlu olduğundan emin olmanızı sağlar.
2. **DoD'yi okuyun**: Her issue'nun **Definition of Done** bölümü vardır. Bu koşullar sağlanmadan PR kabul edilmez.
3. **ROADMAP'i kontrol edin**: [docs/ROADMAP.md](docs/ROADMAP.md) üzerinden mevcut fazları ve öncelikleri görün.

## Geliştirme Ortamı Kurulumu

### Gereksinimler

- **.NET SDK 10.0.400** veya üstü ([indir](https://dotnet.microsoft.com/download))
- **Git 2.40+**
- **IDE** (tercihe göre):
  - JetBrains Rider 2024.3+
  - Visual Studio 2022 17.14+
  - VS Code + C# Dev Kit

### Kurulum

```bash
# Repo'yu klonlayın
git clone https://github.com/ofz-labs/mmf.git
cd mmf

# Bağımlılıkları geri yükleyin
dotnet restore Ofz.Mmf.slnx

# Build edin
dotnet build Ofz.Mmf.slnx

# Testleri çalıştırın
dotnet test Ofz.Mmf.slnx
```

## Branch Stratejisi

`main` branch'i **korumalıdır**. Doğrudan push atılamaz. Her değişiklik kendi branch'inde yapılır ve **Pull Request** ile merge edilir.

### Branch İsimlendirme

Format: `<type>/<kısa-açıklama>`

| Prefix | Ne zaman? | Örnek |
|---|---|---|
| `feat/` | Yeni özellik | `feat/module-loader` |
| `fix/` | Hata düzeltme | `fix/circular-dependency` |
| `chore/` | Yapılandırma, build, tooling | `chore/update-dependencies` |
| `docs/` | Sadece dokümantasyon | `docs/architecture-guide` |
| `test/` | Test ekleme/düzeltme | `test/module-loader-coverage` |
| `refactor/` | Davranış değişmeden yeniden düzenleme | `refactor/extract-validator` |
| `ci/` | CI/CD değişiklikleri | `ci/add-codeql` |

**Kurallar**:
- Küçük harf kullanın
- Kelimeleri `-` ile ayırın
- Kısa ve açıklayıcı olun

## Commit Mesajları

Projede **Conventional Commits** standardı kullanılır. Format:

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Yapı

| Bölüm | Zorunlu mu? | Açıklama |
|---|---|---|
| **Subject** | ✅ Zorunlu | Başlık — 50 karakter ideal, 72 maksimum |
| **Body** | ✅ Zorunlu (anlamlı değişikliklerde) | Neden ve ne yapıldı |
| **Footer** | ⚠️ Issue varsa | `Closes #X`, `BREAKING CHANGE:` |

### Subject (Başlık)

- İmperatif kip: **"add"**, "added" değil
- Küçük harfle başla (type ve scope hariç)
- Nokta koyma sona
- 50 karakter ideal, 72 maksimum

```
feat(core): add MmfModule abstract class
fix(loader): handle circular dependency correctly
docs: update README with usage example
```

### Body (Gövde)

Subject'ten **bir boş satır** sonra başlar. İki amaca hizmet eder:

1. **NEDEN** bu değişiklik yapıldı? (karar gerekçesi)
2. **NE** değişti? (madde madde, gerekirse)

**Örnek — Yeni sınıf ekleme:**
```
feat(core): add MmfException with three constructors

Framework'ün fırlatacağı tüm exception'ların ortak atası.
Kullanıcılar catch (MmfException) yazarak framework hatalarını
diğer .NET exception'larından ayırt edebilir.

- Parameterless: varsayılan exception
- Message: hata mesajı ile
- Message + InnerException: hata zinciri için

Closes #5
```

**Örnek — Mimari karar:**
```
chore: adopt central package management

NuGet paket sürümlerini 10+ projede tek tek güncellemek
sürdürülemez hale geliyordu. CPM ile sürümler tek yerden
yönetiliyor ve transitive dependency çakışmaları önleniyor.

- Directory.Packages.props eklendi
- Tüm .csproj'lardan Version attribute'ları kaldırıldı

Closes #4
```

**Örnek — Bug fix:**
```
fix(loader): prevent duplicate module registration

Aynı modül iki farklı modülden referans aldığında iki kez
yükleniyordu. HashSet ile dedupe edilerek düzeltildi.

Closes #8
```

**Body ne zaman atlanabilir?**
- Küçük düzeltmeler (typo, whitespace)
- Başlık zaten yeterince açıklayıcıysa (nadir)

**Body ne zaman zorunlu?**
- Yeni sınıf/metot ekleme
- Mimari karar içeren değişiklik
- Bug fix — "neden böyle bir hata vardı, nasıl çözüldü"
- Refactor — "neden bu refactor gerekliydi"
- Breaking change — her zaman

### Footer

- `Closes #X` — Issue'yu otomatik kapatır
- `Refs #X` — Issue'ya referans verir (kapatmaz)
- `BREAKING CHANGE: <açıklama>` — Geriye uyumsuz değişiklik

### Tipler

| Tip | Ne zaman? |
|---|---|
| `feat` | Yeni özellik |
| `fix` | Hata düzeltme |
| `docs` | Sadece dokümantasyon |
| `chore` | Yapılandırma, build, tooling |
| `refactor` | Davranış değişmeden kod yeniden düzenleme |
| `test` | Test ekleme/düzeltme |
| `perf` | Performans iyileştirmesi |
| `ci` | CI/CD değişiklikleri |
| `build` | Build sistemi değişiklikleri |

### Şablon Kullanımı

Bu repoda `.gitmessage` şablonu aktiftir. `git commit` yazdığında
editör (VS Code) otomatik açılır ve şablon yüklenir. `#` ile başlayan
satırlar yorumdur — commit mesajına dahil edilmez.

```bash
git add .
git commit          # ← Editör açılır, .gitmessage yüklenir
```

Şablonu ilk kez kullanıyorsanız, Git'e kaydetmeniz gerekebilir:

```bash
git config commit.template .gitmessage
git config core.editor "code --wait"
```

### Kötü vs İyi Commit Örneği

**❌ Kötü (belirsiz başlık):**
```
chore: update files
```
(Neden? Ne değişti? Belirsiz.)

**❌ Kötü (body'siz, bağlam yok):**
```
feat(core): add MmfException
```
(Neden bu sınıf? Nasıl çalışır? Hangi issue? Belirsiz.)

**✅ İyi (başlık + body + footer):**
```
feat(core): add MmfException with three constructors

Framework'ün fırlatacağı tüm exception'ların ortak atası.
Kullanıcılar catch (MmfException) yazarak framework hatalarını
diğer .NET exception'larından ayırt edebilir.

- Parameterless: varsayılan exception
- Message: hata mesajı ile
- Message + InnerException: hata zinciri için

Closes #5
```

## Pull Request Süreci

### 1. Branch'i güncelleyin

```bash
git checkout main
git pull origin main
git checkout your-branch
git rebase main
```

### 2. Değişiklikleri yapın

- Küçük, mantıksal commit'ler halinde ilerleyin
- Her commit **tek bir şey** yapsın
- Commit mesajları Conventional Commits formatında olsun (body dahil)

### 3. Testleri çalıştırın

```bash
dotnet build Ofz.Mmf.slnx
dotnet test Ofz.Mmf.slnx
```

**Kritik**: PR'ınızda CI yeşil olmalı. Kırmızı CI'lı PR'lar merge edilmez.

### 4. Push edin ve PR açın

```bash
git push origin your-branch
```

GitHub'da **Compare & pull request** butonu çıkacak. PR açılırken:
- Başlık Conventional Commits formatında
- PR template'in tüm alanları doldurulmalı
- `Closes #N` ile ilgili issue'ya bağlanmalı

### 5. Review süreci

- Bir reviewer atanır
- Değişiklikler istenirse, aynı branch'e commit atıp push edin
- Yorumlar çözülmeden merge edilmez

### 6. Merge

- **Squash and merge** veya **Rebase and merge** kullanılır
- **Merge commit yasaktır** (ruleset kuralı)

### 7. Merge sonrası temizlik

PR merge edildikten sonra:

```bash
# GitHub'da "Delete branch" butonuna basın (veya terminalden)
git push origin --delete your-branch

# Lokal main'i güncelleyin
git checkout main
git pull origin main

# Lokal branch'i silin
git branch -d your-branch
```

## Kod Stili

Tüm kod stili kuralları `.editorconfig` dosyasında tanımlıdır. IDE'niz bu kuralları otomatik uygular. Özet:

- **Indent**: 4 boşluk (C#), 2 boşluk (XML, YAML, JSON)
- **Satır sonu**: LF
- **`var` kullanımı**: Tip açıkça belli olduğunda (örn. `new List<string>()`)
- **Süslü parantez**: Zorunlu (tek satır bile olsa)
- **`using` sıralaması**: `System.*` en üstte
- **Namespace**: File-scoped (`namespace X;`) — blok-scoped değil
- **Public API'lerde XML dokümantasyon**: Zorunlu

Detaylar için `.editorconfig` dosyasına bakın.

### Analyzer'lar

Projede şu analyzer'lar aktiftir:
- **.NET Roslyn Analyzers** (yerleşik)
- **Roslynator.Analyzers** — kod kalitesi ve okunabilirlik
- **SonarAnalyzer.CSharp** — kod kokusu tespiti

`TreatWarningsAsErrors=true` olduğu için bir **uyarı = build hatası**. PR açmadan önce `dotnet build` yeşil olmalı.

## Test Yazma

- **Unit testler** her yeni özellik için zorunlu
- **xUnit** framework'ü kullanılır
- **AwesomeAssertions** ile assertion yazılır (FluentAssertions 8.x lisans değişikliği nedeniyle AwesomeAssertions'a geçilmiştir)
- Test ismi: `<Metot>_<Senaryo>_<BeklenenDavranış>`
  - Örnek: `MessageConstructor_SetsMessage`
  - Örnek: `LoadModules_WithCircularDependency_ThrowsException`
- Test sınıfları `public sealed class` olmalı (kalıtım yok)
- **Coverage hedefi**: Yeni kod için %80+

### Test Dosyası Başlığı

Her test dosyası, neyi test ettiğini açıklayan bir başlık yorumu ile başlar:

```csharp
// =============================================================================
// MmfExceptionTests
//
// Amaç: MmfException sınıfının constructor'larının ve kalıtım ilişkisinin
//       doğru çalıştığını doğrulamak.
//
// Yaklaşım: Arrange-Act-Assert deseniyle, her constructor için ayrı test.
// =============================================================================
```

### Örnek Test

```csharp
using AwesomeAssertions;
using Ofz.Mmf.Core.Exceptions;

namespace Ofz.Mmf.Core.Tests.Exceptions;

public sealed class MmfExceptionTests
{
    [Fact]
    public void MessageConstructor_SetsMessage()
    {
        // Arrange
        const string message = "Bir şeyler ters gitti";

        // Act
        var exception = new MmfException(message);

        // Assert
        exception.Message.Should().Be(message);
    }
}
```

Testleri çalıştırın:
```bash
dotnet test Ofz.Mmf.slnx
```

Kapsam raporu için:
```bash
dotnet test Ofz.Mmf.slnx --collect:"XPlat Code Coverage"
```

## Sorularınız mı Var?

- **Hata bildirimi**: [Bug template](https://github.com/ofz-labs/mmf/issues/new?template=bug.yml)
- **Özellik önerisi**: [Feature template](https://github.com/ofz-labs/mmf/issues/new?template=feature.yml)

Katkılarınız için tekrar teşekkürler! 🚀

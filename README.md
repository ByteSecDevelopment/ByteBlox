<div align="center">
    <img src="https://github.com/ByteSecDevelopment/ByteBlox/raw/main/Bloxstrap/byteblox.png" width="200">
    <h1>BYTEBLOX</h1>

[![License][badge-repo-license]][repo-license]
[![Workflow][badge-repo-workflow]][repo-actions]
[![Downloads][badge-repo-downloads]][repo-releases]
[![Version][badge-repo-latest]][repo-latest]
[![Discord][badge-discord]][discord-invite]
</div>

> [!CAUTION]
> The only official place to download ByteBlox is this GitHub repository.

*ByteBlox is a custom bootstrapper for Roblox based on Bloxstrap.*
*It aims to provide additional features to compliment your experience.*

***Found any bugs? [Submit an issue](https://github.com/ByteSecDevelopment/ByteBlox/issues/new/choose) or create a bug report in our [Discord server](https://discord.gg/9KmrM64JjC).***

> [!NOTE]
> ByteBlox only supports **Windows 10 and above**.

**Download the latest release [here][repo-latest]**

## Feature List
- Standing standalone EXE (Single File)
- Roblox Studio support
- Framerate cap unlocking
- Global Settings page
- Cache cleaner
- Channel changer

## How to Release
To easily create a new version and upload it to GitHub:
1. Open PowerShell in the project folder.
2. Run the release script:
   ```powershell
   .\Release.ps1 -Version "1.0.0" -Message "Initial Release"
   ```
3. GitHub Actions will automatically build the standalone EXE and create a draft release for you.

[badge-repo-license]:    https://img.shields.io/github/license/ByteSecDevelopment/ByteBlox?style=flat-square
[badge-repo-workflow]:   https://img.shields.io/github/actions/workflow/status/ByteSecDevelopment/ByteBlox/ci-release.yml?branch=main&style=flat-square&label=builds
[badge-repo-downloads]:  https://img.shields.io/github/downloads/ByteSecDevelopment/ByteBlox/latest/total?style=flat-square&color=981bfe
[badge-repo-latest]:     https://img.shields.io/github/v/release/ByteSecDevelopment/ByteBlox?style=flat-square&color=7a39fb

[badge-discord]: https://img.shields.io/discord/1299397064165429360?style=flat-square&logo=discord&logoColor=white&logoSize=auto&label=discord&color=4d3dff

[repo-license]:  https://github.com/ByteSecDevelopment/ByteBlox/blob/main/LICENSE
[repo-actions]:  https://github.com/ByteSecDevelopment/ByteBlox/actions
[repo-releases]: https://github.com/ByteSecDevelopment/ByteBlox/releases
[repo-latest]:   https://github.com/ByteSecDevelopment/ByteBlox/releases/latest

[discord-invite]:  [https://discord.gg/bytesec](https://discord.gg/9KmrM64JjC)

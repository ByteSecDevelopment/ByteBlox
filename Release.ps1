# ByteBlox Release Automation Script
# This script automates the process of updating the version, tagging, and pushing to GitHub.

param (
    [Parameter(Mandatory=$true)]
    [string]$Version,

    [Parameter(Mandatory=$false)]
    [string]$Message = "Release v$Version"
)

$LOG_IDENT = "ReleaseScript"

# 1. Update Bloxstrap.csproj version
Write-Host "[$LOG_IDENT] Updating version to $Version in Bloxstrap.csproj..." -ForegroundColor Cyan
$csprojPath = "Bloxstrap\Bloxstrap.csproj"
$csprojContent = Get-Content $csprojPath
$csprojContent = $csprojContent -replace '<Version>.*</Version>', "<Version>$Version</Version>"
$csprojContent = $csprojContent -replace '<FileVersion>.*</FileVersion>', "<FileVersion>$Version</FileVersion>"
$csprojContent | Set-Content $csprojPath

# 2. Update App.xaml.cs version
Write-Host "[$LOG_IDENT] Updating version to $Version in App.xaml.cs..." -ForegroundColor Cyan
$appPath = "Bloxstrap\App.xaml.cs"
$appContent = Get-Content $appPath
$appContent = $appContent -replace 'public static string Version = ".*";', "public static string Version = `"$Version`";"
$appContent | Set-Content $appPath

# 3. Git Operations
Write-Host "[$LOG_IDENT] Checking Git identity..." -ForegroundColor Cyan
$gitUser = git config user.name
$gitEmail = git config user.email

if ([string]::IsNullOrWhiteSpace($gitUser) -or [string]::IsNullOrWhiteSpace($gitEmail)) {
    Write-Host "[$LOG_IDENT] Git identity not found. Setting default identity for this repository..." -ForegroundColor Yellow
    git config user.name "Grim"
    git config user.email "ByteSecDevelopment@users.noreply.github.com"
}

Write-Host "[$LOG_IDENT] Checking Git remote..." -ForegroundColor Cyan
$remoteUrl = git remote get-url origin
if ($remoteUrl -notlike "*ByteSecDevelopment/ByteBlox*") {
    Write-Host "[$LOG_IDENT] Warning: Remote 'origin' is set to $remoteUrl" -ForegroundColor Yellow
    Write-Host "[$LOG_IDENT] Updating remote 'origin' to point to ByteSecDevelopment/ByteBlox..." -ForegroundColor Cyan
    git remote set-url origin https://github.com/ByteSecDevelopment/ByteBlox.git
}

$currentBranch = git branch --show-current
if ([string]::IsNullOrWhiteSpace($currentBranch)) {
    $currentBranch = "main"
}

Write-Host "[$LOG_IDENT] Preparing changes on branch $currentBranch..." -ForegroundColor Cyan
git add .
$status = git status --porcelain
if ([string]::IsNullOrWhiteSpace($status)) {
    Write-Host "[$LOG_IDENT] No changes detected, skipping commit." -ForegroundColor Yellow
} else {
    Write-Host "[$LOG_IDENT] Committing changes..." -ForegroundColor Cyan
    $commitResult = git commit -m "Build: Bump version to $Version"
    if ($LASTEXITCODE -ne 0 -and $commitResult -notlike "*nothing to commit*") {
        Write-Host "[$LOG_IDENT] Error: Git commit failed!" -ForegroundColor Red
        Write-Host "[$LOG_IDENT] Details: $commitResult" -ForegroundColor Gray
        exit 1
    }
}

# Delete tag if it already exists locally so we can recreate it
if (git tag -l "v$Version") {
    Write-Host "[$LOG_IDENT] Tag v$Version already exists, recreating..." -ForegroundColor Yellow
    git tag -d "v$Version"
}

Write-Host "[$LOG_IDENT] Creating tag v$Version..." -ForegroundColor Cyan
git tag -a "v$Version" -m "$Message"
if ($LASTEXITCODE -ne 0) {
    Write-Host "[$LOG_IDENT] Error: Failed to create tag v$Version!" -ForegroundColor Red
    exit 1
}

Write-Host "[$LOG_IDENT] Pushing to GitHub (branch: $currentBranch)..." -ForegroundColor Cyan
# Use -u to set upstream if it's the first push, and handle potential remote conflicts
git push -u origin "$currentBranch"
if ($LASTEXITCODE -ne 0) {
    Write-Host "[$LOG_IDENT] Push failed. Attempting force push (required for new repositories with existing history)..." -ForegroundColor Yellow
    git push -u origin "$currentBranch" --force
}

Write-Host "[$LOG_IDENT] Pushing tag v$Version..." -ForegroundColor Cyan
git push origin "v$Version" --force
if ($LASTEXITCODE -ne 0) {
    Write-Host "[$LOG_IDENT] Error: Failed to push tag to GitHub!" -ForegroundColor Red
    exit 1
}

Write-Host "[$LOG_IDENT] Done! GitHub Actions will now build the standalone EXE and create a draft release." -ForegroundColor Green
Write-Host "[$LOG_IDENT] You can track the progress at: https://github.com/ByteSecDevelopment/ByteBlox/actions" -ForegroundColor Yellow

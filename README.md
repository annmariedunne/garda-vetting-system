# Secure Web-Based Data Reuse System for Garda Vetting Applications

[![CI - Build and Test](https://github.com/annmariedunne/garda-vetting-system/actions/workflows/ci.yml/badge.svg)](https://github.com/annmariedunne/garda-vetting-system/actions/workflows/ci.yml)
[![CodeQL Security Scan](https://github.com/annmariedunne/garda-vetting-system/actions/workflows/codeql.yml/badge.svg)](https://github.com/annmariedunne/garda-vetting-system/actions/workflows/codeql.yml)

## Overview

This project is a college project developed as part of the the Project Module at ATU.

## Technology Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core (.NET 10) with Razor Pages |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Language | C# |
| Authentication | ASP.NET Identity |
| PDF Generation | QuestPDF |
| UI Framework | Bootstrap / Bootstrap Icons |

## Development Tools

| Tool | Version |
|---|---|
| IDE | Visual Studio 2026 |
| Testing | NUnit |
| ORM Tooling | Entity Framework Core Tools |
| DevOps | GitHub Actions, SonarQube, CodeQL |
| Documentation | DocFX |

---

## Project Status

🚧 **In Development** — Implementation Chapter submitted, Testing Chapter in progress

### Completed
- ✅ GitHub repository setup with branch protection and dev workflow
- ✅ ASP.NET Core Razor Pages project scaffolded with .NET 10
- ✅ Individual Accounts authentication configured
- ✅ Entity Framework Core configured with SQL Server LocalDB
- ✅ Core domain models created: Applicant, ApplicantAddress, AccessCode
- ✅ NUnit test project added with initial model tests
- ✅ GitHub Actions CI/CD pipeline implemented
- ✅ Secure user registration and authentication
- ✅ Personal data storage (Applicant profile)
- ✅ ApplicantAddresses secured with ownership checks and wired to profile
- ✅ Address history displayed on Applicant profile page
- ✅ NUnit tests expanded — ApplicantAddress model and Applicant validation tests
- ✅ Pull request template added
- ✅ AccessCode generation with cryptographically secure 12-character codes
- ✅ AccessCode revocation with audit trail preserved
- ✅ Organisation-side validation page
- ✅ XML doc comments added throughout all models and page models
- ✅ NuGet packages updated to latest versions
- ✅ Post-deletion confirmation flow with option to fully delete Identity account
- ✅ ResidentFrom made nullable with database migration
- ✅ Clipboard copy button for access codes
- ✅ PDF export — applicant profile downloadable as PDF
- ✅ Default pages updated for consistency (Index, Privacy, Error, Layout)
- ✅ Public validate access added — organisations can validate codes without logging in
- ✅ Privacy page updated — GDPR-aware Privacy Notice added
- ✅ EF Core tracking conflict fixed on Applicant and Address edit pages
- ✅ Navbar updated to display applicant first name instead of email address
- ✅ UI responsive layout improvements — all form and display pages updated for mobile, tablet and desktop
- ✅ "Mother's Maiden Name" label corrected across all pages, Applicant.cs string messages and PDF export
- ✅ Resident To hint text added to address pages
- ✅ DocFX documentation site configured and generated

### In Progress
- 🔄 Testing Chapter writeup — deadline 17 May 2026

## Documentation Project Timeline

| Milestone | Target Date | Status |
|---|---|---|
| Introduction Chapter | 22 March 2026 | ✅ |
| Design Chapter | 19 April 2026 | ✅ |
| Implementation Chapter | 3 May 2026 | ✅ |
| Testing Chapter | 17 May 2026 | |
| Final Submission | 20 May 2026 | |

---

## Author

**Student:** Ann Marie Dunne 
**Student No:** L00196623  
**Institution:** ATU — Faculty of Engineering & Technology, Department of Computing

---

*This README will be updated to reflect progress throughout the development lifecycle.*

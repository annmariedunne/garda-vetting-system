using System.Globalization;
using GardaVettingSystem.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GardaVettingSystem.Services
{
    /// <summary>
    /// Generates a PDF document containing an applicant's vetting information.
    /// Used to provide a downloadable export of profile data for organisations
    /// that cannot access the system directly.
    /// </summary>
    public class VettingPdfService
    {
        /// <summary>
        /// Generates a PDF document for the given applicant profile.
        /// </summary>
        /// <param name="applicant">The applicant profile including address history.</param>
        /// <returns>A byte array containing the generated PDF.</returns>
        public byte[] GeneratePdf(Applicant applicant)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Garda Vetting Data Reuse System")
                            .FontSize(18).Bold();
                        col.Item().Text("Applicant Vetting Profile")
                            .FontSize(13).FontColor(Colors.Grey.Darken2);
                        col.Item().PaddingTop(5).LineHorizontal(1);
                    });

                    page.Content().PaddingTop(20).Column(col =>
                    {
                        // Personal Details
                        col.Item().Text("Personal Details").FontSize(13).Bold();
                        col.Item().PaddingTop(5).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            AddRow(table, "Full Name", applicant.FullName);
                            AddRow(table, "Date of Birth", applicant.DateOfBirth?.ToString("d", CultureInfo.InvariantCulture) ?? string.Empty);
                            AddRow(table, "Gender", applicant.Gender);
                            AddRow(table, "Place of Birth", applicant.BirthPlace);
                            AddRow(table, "Surname at Birth", applicant.BirthLastName);
                            AddRow(table, "Mother's Last Name", applicant.MothersLastName);
                        });

                        // Address History
                        col.Item().PaddingTop(20).Text("Address History").FontSize(13).Bold();

                        if (applicant.ApplicantAddresses != null && applicant.ApplicantAddresses.Count > 0)
                        {
                            col.Item().PaddingTop(5).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                // Header row
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Address").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Postcode").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Country").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("From").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("To").Bold();
                                });

                                foreach (var address in applicant.ApplicantAddresses)
                                {
                                    table.Cell().Padding(5).Text(address.AddressLine);
                                    table.Cell().Padding(5).Text(address.Postcode);
                                    table.Cell().Padding(5).Text(address.Country);
                                    table.Cell().Padding(5).Text(address.ResidentFrom?.ToString(CultureInfo.InvariantCulture) ?? string.Empty);
                                    table.Cell().Padding(5).Text(address.ResidentTo.HasValue ? address.ResidentTo.Value.ToString(CultureInfo.InvariantCulture)! : "Current");
                                }
                            });
                        }
                        else
                        {
                            col.Item().PaddingTop(5).Text("No address history available.")
                                .FontColor(Colors.Grey.Darken1);
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Generated by Garda Vetting Data Reuse System — ");
                        text.Span(DateTimeOffset.UtcNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    });
                });
            }).GeneratePdf();
        }

        /// <summary>
        /// Adds a label/value row to a two-column table.
        /// </summary>
        private static void AddRow(TableDescriptor table, string label, string value)
        {
            table.Cell().Padding(4).Text(label).Bold();
            table.Cell().Padding(4).Text(value);
        }
    }
}

CREATE VIEW [dbo].[CommunityPartnerView] AS
  SELECT
        Prefix,
        FirstName,
        MiddleName,
        LastName,
        Suffix,
        OfficePhoneNumber,
        CellPhoneNumber,
        FaxPhoneNumber,
        c.EmailAddress as ContactEmailAddress,
        p.AddressLine1 as OrganizationAddressLine1,
        p.AddressLine2 as OrganizationAddressLine2,
        p.City as OrganizationCity,
        p.State as OrganizationState,
        p.Zipcode as OrganizationZipcode,
        p.Name as CommunityPartnerName
    FROM
        CommunityPartner AS P
    LEFT JOIN
        CommunityPartnerContact AS c ON c.CommunityPartnerName = p.Name

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Tc.Crm.CustomWorkflowSteps
{
    public class PayloadBooking
    {        

        public const string AccountType = "B";

        public const string ContactType = "P";

        public Address AddressInfo { get; set; }

        public Booking BookingInfo { get; set; }

        public Customer CustomerInfo { get; set; }

        public Remark RemarkInfo { get; set; }

        //public Accommodation AccommdationkInfo { get; set; }




        //------------------------------------------------------------------------------
        // <auto-generated>
        //     This code was generated by a tool.
        //     Runtime Version:4.0.30319.42000
        //
        //     Changes to this file may cause incorrect behavior and will be lost if
        //     the code is regenerated.
        // </auto-generated>
        //------------------------------------------------------------------------------
        [DataContract(Name = "address")]
        public class Address
        {

            [DataMember(Name = "additionalAddressInfo")]
            public string AdditionalAddressInfo { get; set; }

            [DataMember(Name = "flatNumberUnit")]
            public string FlatNumberUnit { get; set; }

            [DataMember(Name = "houseNumberBuilding")]
            public string HouseNumberBuilding { get; set; }

            [DataMember(Name = "box")]
            public string Box { get; set; }

            [DataMember(Name = "town")]
            public string Town { get; set; }

            [DataMember(Name = "country")]
            public string Country { get; set; }

            [DataMember(Name = "county")]
            public string County { get; set; }

            [DataMember(Name = "number")]
            public string Number { get; set; }

            [DataMember(Name = "postalCode")]
            public string PostalCode { get; set; }

            [DataMember(Name = "street")]
            public string Street { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }
        }

        [DataContract(Name = "bookingIdentifier")]
        public class BookingIdentifier
        {

            [DataMember(Name = "sourceMarket")]
            public string SourceMarket { get; set; }

            [DataMember(Name = "sourceSystem")]
            public string SourceSystem { get; set; }

            [DataMember(Name = "bookingNumber")]
            public string BookingNumber { get; set; }

            [DataMember(Name = "bookingVersionOnTour")]
            public string BookingVersionOnTour { get; set; }

            [DataMember(Name = "bookingVersionTourOperator")]
            public string BookingVersionTourOperator { get; set; }

            [DataMember(Name = "bookingUpdateDateOnTour")]
            public string BookingUpdateDateOnTour { get; set; }

            [DataMember(Name = "bookingUpdateDateTourOperator")]
            public string BookingUpdateDateTourOperator { get; set; }
        }

        [DataContract(Name = "bookingGeneral")]
        public class BookingGeneral
        {

            [DataMember(Name = "bookingStatus")]
            public string BookingStatus { get; set; }

            [DataMember(Name = "bookingDate")]
            public string BookingDate { get; set; }

            [DataMember(Name = "departureDate")]
            public string DepartureDate { get; set; }

            [DataMember(Name = "returnDate")]
            public string ReturnDate { get; set; }

            [DataMember(Name = "duration")]
            public string Duration { get; set; }

            [DataMember(Name = "destination")]
            public string Destination { get; set; }

            [DataMember(Name = "toCode")]
            public string ToCode { get; set; }

            [DataMember(Name = "brand")]
            public string Brand { get; set; }

            [DataMember(Name = "brochureCode")]
            public string BrochureCode { get; set; }

            [DataMember(Name = "isLateBooking")]
            public bool IsLateBooking { get; set; }

            [DataMember(Name = "numberofParticipants")]
            public int NumberofParticipants { get; set; }

            [DataMember(Name = "numberOfAdults")]
            public int NumberOfAdults { get; set; }

            [DataMember(Name = "numberOfChildren")]
            public int NumberOfChildren { get; set; }

            [DataMember(Name = "numberOfInfants")]
            public int NumberOfInfants { get; set; }

            [DataMember(Name = "travelAmount")]
            public decimal TravelAmount { get; set; }

            [DataMember(Name = "currency")]
            public string Currency { get; set; }

            [DataMember(Name = "hasComplaint")]
            public bool HasComplaint { get; set; }
        }

        [DataContract(Name = "bookingIdentity")]
        public class BookingIdentity
        {

            [DataMember(Name = "booker")]
            public Booking_Booker Booker { get; set; }
        }

        [DataContract(Name = "Remark")]
        public class Remark
        {

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "text")]
            public string Text { get; set; }
        }

        [DataContract(Name = "travelParticipant")]
        public class TravelParticipant
        {

            [DataMember(Name = "firstName")]
            public string FirstName { get; set; }

            [DataMember(Name = "lastName")]
            public string LastName { get; set; }

            [DataMember(Name = "age")]
            public int Age { get; set; }

            [DataMember(Name = "gender")]
            public string Gender { get; set; }

            [DataMember(Name = "relation")]
            public string Relation { get; set; }

            [DataMember(Name = "travelParticipantIDOnTour")]
            public string TravelParticipantIDOnTour { get; set; }

            [DataMember(Name = "language")]
            public string Language { get; set; }

            [DataMember(Name = "birthDate")]
            public string BirthDate { get; set; }

            [DataMember(Name = "Remark")]
            public Remark[] Remark { get; set; }
        }

        [DataContract(Name = "travelParticipantAssignment")]
        public class TravelParticipantAssignment
        {

            [DataMember(Name = "travelParticipantID")]
            public string TravelParticipantID { get; set; }
        }

        [DataContract(Name = "brands")]
        public class Brands
        {
        }

        [DataContract(Name = "tourguide")]
        public class Tourguide
        {

            [DataMember(Name = "tourguideID")]
            public string TourguideID { get; set; }

            [DataMember(Name = "tourguideName")]
            public string TourguideName { get; set; }

            [DataMember(Name = "brands")]
            public Brands[] Brands { get; set; }
        }

        [DataContract(Name = "tourguideAssignment")]
        public class TourguideAssignment
        {

            [DataMember(Name = "tourguide")]
            public Tourguide Tourguide { get; set; }
        }

        [DataContract(Name = "accommodation")]
        public class Accommodation
        {

            [DataMember(Name = "accommodationCode")]
            public string AccommodationCode { get; set; }

            [DataMember(Name = "groupAccommodationCode")]
            public string GroupAccommodationCode { get; set; }

            [DataMember(Name = "accommodationDescription")]
            public string AccommodationDescription { get; set; }

            [DataMember(Name = "order")]
            public int Order { get; set; }

            [DataMember(Name = "startDate")]
            public string StartDate { get; set; }

            [DataMember(Name = "endDate")]
            public string EndDate { get; set; }

            [DataMember(Name = "roomType")]
            public string RoomType { get; set; }

            [DataMember(Name = "boardType")]
            public string BoardType { get; set; }

            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "hasSharedRoom")]
            public bool HasSharedRoom { get; set; }

            [DataMember(Name = "numberOfParticipants")]
            public int NumberOfParticipants { get; set; }

            [DataMember(Name = "numberOfRooms")]
            public int NumberOfRooms { get; set; }

            [DataMember(Name = "withTransfer")]
            public bool WithTransfer { get; set; }

            [DataMember(Name = "isExternalService")]
            public bool IsExternalService { get; set; }

            [DataMember(Name = "externalServiceCode")]
            public string ExternalServiceCode { get; set; }

            [DataMember(Name = "notificationRequired")]
            public bool NotificationRequired { get; set; }

            [DataMember(Name = "needsTourGuideAssignment")]
            public bool NeedsTourGuideAssignment { get; set; }

            [DataMember(Name = "isExternalTransfer")]
            public bool IsExternalTransfer { get; set; }

            [DataMember(Name = "transferServiceLevel")]
            public string TransferServiceLevel { get; set; }

            [DataMember(Name = "travelParticipantAssignment")]
            public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

            [DataMember(Name = "remark")]
            public Remark[] Remark { get; set; }

            [DataMember(Name = "tourguideAssignment")]
            public TourguideAssignment TourguideAssignment { get; set; }
        }

        [DataContract(Name = "transport")]
        public class Transport
        {

            [DataMember(Name = "transportCode")]
            public string TransportCode { get; set; }

            [DataMember(Name = "transportDescription")]
            public string TransportDescription { get; set; }

            [DataMember(Name = "order")]
            public int Order { get; set; }

            [DataMember(Name = "startDate")]
            public string StartDate { get; set; }

            [DataMember(Name = "endDate")]
            public string EndDate { get; set; }

            [DataMember(Name = "transferType")]
            public string TransferType { get; set; }

            [DataMember(Name = "departureAirport")]
            public string DepartureAirport { get; set; }

            [DataMember(Name = "arrivalAirport")]
            public string ArrivalAirport { get; set; }

            [DataMember(Name = "carrierCode")]
            public string CarrierCode { get; set; }

            [DataMember(Name = "flightNumber")]
            public string FlightNumber { get; set; }

            [DataMember(Name = "flightIdentifier")]
            public string FlightIdentifier { get; set; }

            [DataMember(Name = "numberOfParticipants")]
            public int NumberOfParticipants { get; set; }

            [DataMember(Name = "travelParticipantAssignment")]
            public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

            [DataMember(Name = "remark")]
            public Remark[] Remark { get; set; }
        }

        [DataContract(Name = "transfer")]
        public class Transfer
        {

            [DataMember(Name = "transferCode")]
            public string TransferCode { get; set; }

            [DataMember(Name = "transferDescription")]
            public string TransferDescription { get; set; }

            [DataMember(Name = "order")]
            public int Order { get; set; }

            [DataMember(Name = "startDate")]
            public string StartDate { get; set; }

            [DataMember(Name = "category")]
            public string Category { get; set; }

            [DataMember(Name = "endDate")]
            public string EndDate { get; set; }

            [DataMember(Name = "transferType")]
            public string TransferType { get; set; }

            [DataMember(Name = "departureAirport")]
            public string DepartureAirport { get; set; }

            [DataMember(Name = "arrivalAirport")]
            public string ArrivalAirport { get; set; }

            [DataMember(Name = "travelParticipantAssignment")]
            public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

            [DataMember(Name = "remark")]
            public Remark[] Remark { get; set; }
        }

        [DataContract(Name = "extraServiceCode")]
        public class ExtraServiceCode
        {
        }

        [DataContract(Name = "extraServiceDescription")]
        public class ExtraServiceDescription
        {
        }

        [DataContract(Name = "extraService")]
        public class ExtraService
        {

            [DataMember(Name = "extraServiceCode")]
            public ExtraServiceCode ExtraServiceCode { get; set; }

            [DataMember(Name = "extraServiceDescription")]
            public ExtraServiceDescription ExtraServiceDescription { get; set; }

            [DataMember(Name = "order")]
            public int Order { get; set; }

            [DataMember(Name = "startDate")]
            public string StartDate { get; set; }

            [DataMember(Name = "endDate")]
            public string EndDate { get; set; }

            [DataMember(Name = "travelParticipantAssignment")]
            public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

            [DataMember(Name = "remark")]
            public Remark[] Remark { get; set; }
        }

        [DataContract(Name = "services")]
        public class Services
        {

            [DataMember(Name = "accommodation")]
            public Accommodation[] Accommodation { get; set; }

            [DataMember(Name = "transport")]
            public Transport[] Transport { get; set; }

            [DataMember(Name = "transfer")]
            public Transfer[] Transfer { get; set; }

            [DataMember(Name = "extraService")]
            public ExtraService[] ExtraService { get; set; }
        }

        [DataContract(Name = "customerIdentifier")]
        public class CustomerIdentifier
        {

            [DataMember(Name = "customerID")]
            public string CustomerID { get; set; }

            [DataMember(Name = "businessArea")]
            public string BusinessArea { get; set; }

            [DataMember(Name = "sourceMarket")]
            public string SourceMarket { get; set; }

            [DataMember(Name = "sourceSystem")]
            public string SourceSystem { get; set; }
        }

        [DataContract(Name = "customerGeneral")]
        public class CustomerGeneral
        {

            [DataMember(Name = "customerStatus")]
            public string CustomerStatus { get; set; }

            [DataMember(Name = "customerType")]
            public string CustomerType { get; set; }
        }

        [DataContract(Name = "customerIdentity")]
        public class CustomerIdentity
        {

            [DataMember(Name = "salutation")]
            public string Salutation { get; set; }

            [DataMember(Name = "academictitle")]
            public string Academictitle { get; set; }

            [DataMember(Name = "firstName")]
            public string FirstName { get; set; }

            [DataMember(Name = "middleName")]
            public string MiddleName { get; set; }

            [DataMember(Name = "lastName")]
            public string LastName { get; set; }

            [DataMember(Name = "language")]
            public string Language { get; set; }

            [DataMember(Name = "gender")]
            public string Gender { get; set; }

            [DataMember(Name = "birthdate")]
            public string Birthdate { get; set; }
        }

        [DataContract(Name = "company")]
        public class Company
        {

            [DataMember(Name = "companyName")]
            public string CompanyName { get; set; }
        }

        [DataContract(Name = "additional")]
        public class Additional
        {

            [DataMember(Name = "segment")]
            public string Segment { get; set; }

            [DataMember(Name = "dateOfdeath")]
            public string DateOfdeath { get; set; }
        }

        [DataContract(Name = "phone")]
        public class Phone
        {

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "number")]
            public string Number { get; set; }
        }

        [DataContract(Name = "email")]
        public class Email
        {

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }
        }

        [DataContract(Name = "social")]
        public class Social
        {

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "value")]
            public string Value { get; set; }
        }

        [DataContract(Name = "customer")]
        public class Customer
        {

            [DataMember(Name = "customerIdentifier")]
            public CustomerIdentifier CustomerIdentifier { get; set; }

            [DataMember(Name = "customerGeneral")]
            public CustomerGeneral CustomerGeneral { get; set; }

            [DataMember(Name = "customerIdentity")]
            public CustomerIdentity CustomerIdentity { get; set; }

            [DataMember(Name = "company")]
            public Company Company { get; set; }

            [DataMember(Name = "additional")]
            public Additional Additional { get; set; }

            [DataMember(Name = "address")]
            public Address[] Address { get; set; }

            [DataMember(Name = "phone")]
            public Phone[] Phone { get; set; }

            [DataMember(Name = "email")]
            public Email[] Email { get; set; }

            [DataMember(Name = "social")]
            public Social[] Social { get; set; }
        }

        [DataContract(Name = "booking")]
        public class Booking
        {

            [DataMember(Name = "bookingIdentifier")]
            public BookingIdentifier BookingIdentifier { get; set; }

            [DataMember(Name = "bookingGeneral")]
            public BookingGeneral BookingGeneral { get; set; }

            [DataMember(Name = "bookingIdentity")]
            public BookingIdentity BookingIdentity { get; set; }

            [DataMember(Name = "travelParticipant")]
            public TravelParticipant[] TravelParticipant { get; set; }

            [DataMember(Name = "services")]
            public Services Services { get; set; }

            [DataMember(Name = "customer")]
            public Customer Customer { get; set; }

            [DataMember(Name = "remark")]
            public Remark[] Remark { get; set; }
        }

        [DataContract(Name = "booking_booker")]
        public class Booking_Booker
        {
            [DataMember(Name = "address")]
            public Address Address { get; set; }
            [DataMember(Name = "email")]
            public string Email { get; set; }
            [DataMember(Name = "phone")]
            public string Phone { get; set; }
            [DataMember(Name = "mobile")]
            public string Mobile { get; set; }
            [DataMember(Name = "emergencyNumber")]
            public string EmergencyNumber { get; set; }
        }



    }

}




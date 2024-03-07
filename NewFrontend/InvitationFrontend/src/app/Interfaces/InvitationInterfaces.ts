export interface SubmitRSVPInputDTO {
    email: string;
    firstName: string;
    lastName: string;
    songRequest?: string;
    isAccepted: boolean;
}

export interface GetAllInvitationsOutputDTO {
    id: string; // Guids are typically represented as strings in TypeScript
    email: string;
    firstName: string;
    lastName: string;
    songRequest?: string; // The question mark indicates that this field is optional
    isAccepted: boolean;
}

  
export interface SubmitRSVPInputDTO {
    email: string;
    firstName: string;
    lastName: string;
    songRequest?: string;
    isAccepted: boolean;
}

export interface Invitation {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    songRequest?: string | null;
    isAccepted: boolean;
}

export interface GetAllInvitationsOutputDTO {
    invitations: Invitation[] | null;
    totalRecords: number;
}


  
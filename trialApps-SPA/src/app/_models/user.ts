import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    age: number;
    gender: string;
    createdDate: Date;
    lastModified: Date;
    lastActive: Date;
    photoURL: string;
    city: string;
    country: string;
    interests?: string;
    intro?: string;
    lookingFor?: string;
    photos?: Photo[];
}


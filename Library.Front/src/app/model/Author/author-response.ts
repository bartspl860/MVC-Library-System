import { BookResponse } from "../Book/book-response";

export interface AuthorResponse{
    id: number;
    name: string;
    surname: string;
    dateOfBirth: Date;
}
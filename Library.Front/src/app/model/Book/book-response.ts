import { AuthorResponse } from "../Author/author-response";
import { PublishingHouseResponse } from "../PublishingHouse/publishing-house-response";

export interface BookResponse{
    id: number;
    title: string;
    authors: AuthorResponse[];
    publishingHouse: PublishingHouseResponse;
}
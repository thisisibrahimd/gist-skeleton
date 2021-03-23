-- Ibrahim Diallo

CREATE TABLE public.eligibility_criterion
(
    nct_id character varying(255) NOT NULL,
    concept_name character varying(255),
    concept_id integer NOT NULL,
    domain_id character varying(255) NOT NULL,
    cat_elig integer NOT NULL,
    lab_elig_min integer,
    lab_elig_max integer,
    PRIMARY KEY (nct_id, concept_id),
    CONSTRAINT ck_domain_id CHECK (domain_id = 'Measurement' or domain_id = 'Observation' or domain_id = 'Procedure' or domain_id = 'Condition' or domain_id = 'Drug'),
    CONSTRAINT ck_lab_elig_min_max CHECK ((lab_elig_min is null and lab_elig_max is null) or not (lab_elig_min is null and lab_elig_max is null))
);
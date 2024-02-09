// import node module libraries
import React, { Fragment, useEffect } from 'react'

// import sub components
import HeroRightImage from './HeroRightImage';
import ExploreSkillCourses from './ExploreSkillCourses';
import BuildingSkills from './BuildingSkills';
import LearnLatestSkills from './LearnLatestSkills';
import FeaturesWithBullets from './FeaturesWithBullets';
import UpcomingWebinars from './UpcomingWebinars';
import FindRightCourse from './FindRightCourse';

// import custom components
import LogosTopHeading3 from 'components/marketing/common/clientlogos/LogosTopHeading3';

// import layouts
import NavbarLanding from 'layouts/marketing/navbars/NavbarLanding';
import FooterCenter from 'layouts/marketing/footers/FooterWithLinks';

// import data files
import LogoList2 from 'data/marketing/clientlogos/LogoList2';

const LandingEducation = () => {
  useEffect(() => {
    document.body.className = 'bg-white';
  });

  return (
    <Fragment>
      {/*   Landing Page Navbar */}
      <NavbarLanding center />

      {/*   section  */}
      <main>

        {/*  learn today hero section */}
        <HeroRightImage />

        {/*  trusted */}
        <LogosTopHeading3
          title="TRUSTED BY OVER 12,500 GREAT TEAMS"
          logos={LogoList2}
          limit={5}
        />

        {/*  Explore skill courses */}
        <ExploreSkillCourses />

        {/*  Building strong foundational skills */}
        <BuildingSkills />

        {/*  Learn latest Skills */}
        <LearnLatestSkills />

        {/*  Features With Bullets */}
        <hr className="my-0 bg-transparent text-white" />
        <FeaturesWithBullets />

        {/*  upcoming webinars */}
        <UpcomingWebinars />

        {/*  find right course */}
        <FindRightCourse />

      </main>

      {/*   Footer with center alignment */}
      <FooterCenter />

    </Fragment>
  )
}

export default LandingEducation